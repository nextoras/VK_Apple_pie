'Openpose', human pose estimation algorithm, have been implemented using Tensorflow. It also provides several variants that have some changes to the network structure for **real-time processing on the CPU or low-power embedded devices.**

**You can even run this on your macbook with a descent FPS!**

Original Repo(Caffe) : https://github.com/CMU-Perceptual-Computing-Lab/openpose

### Before all
In this case prefer to use conda with preinstalled ```torch``` , ```torchvision```, ```cv2```
Cause we used pretrained models from torchvision

### Install

Clone the repo and install 3rd-party libraries.

```bash
$ pip3 install -r requirements.txt
```

Build c++ library for post processing
```
$ cd tf_pose/pafprocess
$ swig -python -c++ pafprocess.i && python3 setup.py build_ext --inplace
```

### Download Tensorflow Graph File(pb file)

Before running demo, you should download graph files. You can deploy this graph on your mobile or other platforms.

- cmu (trained in 656x368)
- mobilenet_thin (trained in 432x368)
- mobilenet_v2_large (trained in 432x368)
- mobilenet_v2_small (trained in 432x368)

CMU's model graphs are too large for git, so I uploaded them on an external cloud. You should download them if you want to use cmu's original model. Download scripts are provided in the model folder.

```
$ cd models/graph/cmu
$ bash download.sh
```

### Runing
```python3 test.py```