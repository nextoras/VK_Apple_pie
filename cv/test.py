from torchvision import models
from PIL import Image
import matplotlib.pyplot as plt
import torch
import math

from tf_pose import common
import cv2
import numpy as np
from tf_pose.estimator import TfPoseEstimator
from tf_pose.networks import get_graph_path, model_wh

import flask
import json

# Apply the transformations needed
import torchvision.transforms as T

MAX_DIMENSION = math.pi / 18
HUMAN_HEIGHT = 179


class Point:
    def __init__(self, x, y):
        self.x = x
        self.y = y


# Define the helper function
def decode_segmap(image, source, nc=21):
    label_colors = np.array([(0, 0, 0),  # 0=background
                             # 1=aeroplane, 2=bicycle, 3=bird, 4=boat, 5=bottle
                             (128, 0, 0), (0, 128, 0), (128, 128, 0), (0, 0, 128), (128, 0, 128),
                             # 6=bus, 7=car, 8=cat, 9=chair, 10=cow
                             (0, 128, 128), (128, 128, 128), (64, 0, 0), (192, 0, 0), (64, 128, 0),
                             # 11=dining table, 12=dog, 13=horse, 14=motorbike, 15=person
                             (192, 128, 0), (64, 0, 128), (192, 0, 128), (64, 128, 128), (192, 128, 128),
                             # 16=potted plant, 17=sheep, 18=sofa, 19=train, 20=tv/monitor
                             (0, 64, 0), (128, 64, 0), (0, 192, 0), (128, 192, 0), (0, 64, 128)])

    r = np.zeros_like(image).astype(np.uint8)
    g = np.zeros_like(image).astype(np.uint8)
    b = np.zeros_like(image).astype(np.uint8)

    idx = image == 15

    r[idx] = label_colors[15, 0]
    g[idx] = label_colors[15, 1]
    b[idx] = label_colors[15, 2]

    rgb = np.stack([r, g, b], axis=2)

    # Load the foreground input image
    foreground = cv2.imread(source)

    # Change the color of foreground image to RGB
    # and resize image to match shape of R-band in RGB output map
    foreground = cv2.cvtColor(foreground, cv2.COLOR_BGR2RGB)
    foreground = cv2.resize(foreground, (r.shape[1], r.shape[0]))

    # Convert uint8 to float
    foreground = foreground.astype(float)

    # Create a binary mask of the RGB output map using the threshold value 0
    th, alpha = cv2.threshold(np.array(rgb), 0, 255, cv2.THRESH_BINARY)

    # Apply a slight blur to the mask to soften edges
    alpha = cv2.GaussianBlur(alpha, (7, 7), 0)

    # Normalize the alpha mask to keep intensity between 0 and 1
    alpha = alpha.astype(float) / 255

    # Multiply the foreground with the alpha matte
    foreground = cv2.multiply(alpha, foreground)

    # Return a normalized output image for display
    return foreground / 255


def segment(net, img_path, show_orig=True, dev='cpu'):
    img = Image.open(img_path)
    # Comment the Resize and CenterCrop for better inference results
    trf = T.Compose([T.Resize(450),
                     # T.CenterCrop(224),
                     T.ToTensor(),
                     T.Normalize(mean=[0.485, 0.456, 0.406],
                                 std=[0.229, 0.224, 0.225])])
    inp = trf(img).unsqueeze(0).to(dev)
    out = net.to(dev)(inp)['out']
    om = torch.argmax(out.squeeze(), dim=0).detach().cpu().numpy()

    rgb = decode_segmap(om, img_path)

    sizeY, sizeX, sizeZ = rgb.shape
    minW, minH, maxW, maxH = [sizeX, sizeY, 0, 0]
    for y in range(0, sizeY):
        for x in range(0, sizeX):
            is_background = True
            for z in range(0, sizeZ):
                if rgb[y, x, z] > 0:
                    is_background = False

            if not is_background:
                if x < minW:
                    minW = x
                elif x > maxW:
                    maxW = x
                elif y < minH:
                    minH = y
                elif y > maxH:
                    maxH = y

    print(sizeX, sizeY)
    print('Width params', minW, maxW)
    print('Height params', minH, maxH)

    plt.imsave('../data/new.png', rgb)
    return sizeX, sizeY, minW, minH, maxW, maxH, rgb


def height_in_pic(y, pic_height):
    return int(pic_height * y + 0.5)


def width_in_pic(x, pic_width):
    return int(pic_width * x + 0.5)


def get_vector_size(x1, x2, y1, y2):
    return math.sqrt((x1 - x2) ** 2 + (y1 - y2) ** 2)


def arm_size(arm_parts, end_of_hand, body_parts, sizeX, sizeY):
    """In arm_parts: Shoulder, cubit, hand"""
    arm_shoulder = get_vector_size(
        width_in_pic(body_parts[arm_parts[1]].x, sizeX),
        width_in_pic(body_parts[arm_parts[0]].x, sizeX),
        height_in_pic(body_parts[arm_parts[1]].y, sizeY),
        height_in_pic(body_parts[arm_parts[0]].y, sizeY)
    )

    arm = get_vector_size(
        width_in_pic(body_parts[arm_parts[2]].x, sizeX),
        width_in_pic(body_parts[arm_parts[1]].x, sizeX),
        height_in_pic(body_parts[arm_parts[2]].y, sizeY),
        height_in_pic(body_parts[arm_parts[1]].y, sizeY)
    )

    hand = math.fabs(width_in_pic(body_parts[arm_parts[2]].x, sizeX) - end_of_hand)

    return arm_shoulder + arm + hand


def get_head_border(rgb, x_coordinate):
    border = len(rgb)

    for y in range(0, len(rgb)):
        for color in range(0, len(rgb[y][x_coordinate])):
            if rgb[y][x_coordinate][color] > 0:
                if border > y:
                    border = y
                    break

    return border


def get_size(rgb, level, left_bound, right_bound):
    for x in range(0, len(rgb[level])):
        for color in range(0, len(rgb[level][x])):
            if rgb[level][x][color] > 0.1:
                if x < left_bound:
                    left_bound = x
                if x > right_bound:
                    right_bound = x

    return left_bound, right_bound


def get_front_sizes(human_height = HUMAN_HEIGHT):
    dlab = models.segmentation.deeplabv3_resnet101(pretrained=True).eval()

    sizeX, sizeY, minW, minH, maxW, maxH, rgb = segment(dlab, '../data/6.jpeg', show_orig=True)

    default_height = 432
    defult_width = 368
    default_model = 'cmu'
    default_resize = 4.0

    img = common.read_imgfile('../data/new.png')
    e = TfPoseEstimator(get_graph_path(default_model), target_size=(default_height, defult_width))
    humans = e.inference(img, resize_to_default=False, upsample_size=default_resize)

    body_parts = humans[0].body_parts
    parts_coordinates = {}

    hand_size_left = arm_size([2, 3, 4], minW, body_parts, sizeX, sizeY)
    hand_size_right = arm_size([5, 6, 7], maxW, body_parts, sizeX, sizeY)

    height_on_image = maxH - minH
    human_in_pix = human_height / height_on_image

    shoulders = height_in_pic((body_parts[2].y + body_parts[5].y) / 2, sizeY)
    hips = int(height_in_pic((body_parts[8].y + body_parts[11].y) / 2, sizeY))

    # waist select
    waist = int(shoulders + (hips - shoulders) / 3 * 2)

    print(waist)
    waist_left_bound = maxW
    waist_right_bound = 0

    waist_left_bound, waist_right_bound = get_size(rgb, waist, waist_left_bound, waist_right_bound)
    parts_coordinates[20] = Point(waist_left_bound, waist)
    parts_coordinates[21] = Point(waist_right_bound, waist)
    waist_level = human_in_pix * (waist - minH)
    waist = human_in_pix * (waist_right_bound - waist_left_bound)

    # chest select
    chest = int(shoulders + (hand_size_left + hand_size_right) / 10)
    chest_left_bound = maxW
    chest_right_bound = 0

    print(chest)
    chest_left_bound, chest_right_bound = get_size(rgb, chest, chest_left_bound, chest_right_bound)
    parts_coordinates[18] = Point(chest_left_bound, chest)
    parts_coordinates[19] = Point(chest_right_bound, chest)
    chest_level = human_in_pix * (chest - minH)
    chest = human_in_pix * (chest_right_bound - chest_left_bound)

    # hips select
    hips_left_bound = maxW
    hips_right_bound = 0

    hips_left_bound, hips_right_bound = get_size(rgb, hips, hips_left_bound, hips_right_bound)
    parts_coordinates[23] = Point(hips_left_bound, hips)
    parts_coordinates[24] = Point(hips_right_bound, hips)
    hips_level = human_in_pix * (hips - minH)
    hips = human_in_pix * (hips_right_bound - hips_left_bound)

    # leg length
    leg_length = (height_in_pic(body_parts[10].y, sizeY) - height_in_pic(body_parts[9].y, sizeY) + height_in_pic(body_parts[13].y, sizeY) - height_in_pic(body_parts[12].y, sizeY))
    leg = human_in_pix * leg_length

    for part in body_parts:
        part

    return chest, hips, waist, leg, chest_level, hips_level, waist_level


def get_side_sizes(chest_level, hips_level, waist_level, human_height = HUMAN_HEIGHT):
    dlab = models.segmentation.deeplabv3_resnet101(pretrained=True).eval()

    sizeX, sizeY, minW, minH, maxW, maxH, rgb = segment(dlab, '../data/7.jpeg', show_orig=True)

    human_in_pix = human_height / (maxH - minH)

    left_bound, right_bound = get_size(rgb, int(waist_level / human_in_pix) + minH, sizeX, 0)
    waist = human_in_pix * (right_bound - left_bound)

    left_bound, right_bound = get_size(rgb, int(chest_level / human_in_pix) + minH, sizeX, 0)
    chest = human_in_pix * (right_bound - left_bound)

    left_bound, right_bound = get_size(rgb, int(hips_level / human_in_pix) + minH, sizeX, 0)
    hips = human_in_pix * (right_bound - left_bound)

    return chest, hips, waist


def get_perimetr(a, b):
    return 2 * math.pi * math.sqrt((a * a + b * b) / 8)

#server
UPLOAD_FOLDER = '../data'
ALLOWED_EXTENSIONS = {'jpg', 'jpeg'}

app = flask.Flask(__name__)
app.config['UPLOAD_FOLDER'] = UPLOAD_FOLDER


def to_json(data):
    return json.dumps(data) + "\n"

def resp(code, data):
    return flask.Response(
        status=code,
        mimetype="application/json",
        response=to_json(data)
    )    


@app.route('/')
def root():
    return flask.redirect('/api/themes')

# e.g. failed to parse json
@app.errorhandler(400)
def page_not_found(e):
    return resp(400, {})


@app.errorhandler(404)
def page_not_found(e):
    return resp(400, {})


@app.errorhandler(405)
def page_not_found(e):
    return resp(405, {})

@app.route('/', methods=['POST'])
def upload_file():
    # check if the post request has the file part
    if 'front' not in flask.request.files or 'side' not in flask.request.files:
        flask.flash('No file part')
        return flask.redirect(flask.request.url)
    front = flask.request.files['file']
    side = flask.request.files['side']
    # if user does not select file, browser also
    # submit an empty part without filename
    if front.filename == '' or side.filename == '':
        flask.flash('No selected file')
        return redirect(request.url)
    if front and allowed_file(front.filename):
        filename = secure_filename('6.jpeg')
        front.save(os.path.join(app.config['UPLOAD_FOLDER'], filename))
    if side and allowed_file(side.filename):
        filename = secure_filename('7.jpeg')
        front.save(os.path.join(app.config['UPLOAD_FOLDER'], filename))
    chest, hips, waist, leg, chest_level, hips_level, waist_level = get_front_sizes(flask.request.height)
    print(chest, hips, waist, leg, chest_level, hips_level, waist_level)
    
    chest_side, hips_side, waist_side = get_side_sizes(chest_level, hips_level, waist_level, flask.request.height)
    print(chest_side, hips_side, waist_side)
    
    chest = get_perimetr(chest, chest_side)
    hips = get_perimetr(hips, hips_side)
    waist = get_perimetr(waist, waist_side)
    
    print(chest, waist, hips)
    return resp(200, {"chest": chest, "wips": wips, "hips": hips, "leg": leg})


app.run()