import * as React from 'react';
import { connect, ConnectedProps } from "react-redux";

import {Panel, PanelHeader, Group, Cell, Div, Text, Link, File, Input, FormLayoutGroup, Button} from '@vkontakte/vkui';
import {UserInfo} from "@vkontakte/vk-bridge";
import Icon28HelpOutline from '@vkontakte/icons/dist/28/help_outline';
import Icon24AddOutline from '@vkontakte/icons/dist/24/add_outline';
import Icon24Camera from '@vkontakte/icons/dist/24/camera_outline';

import frontSize from '../../assets/frontSize.png';
import sideSize from '../../assets/sideSize.png';

import './GetSizes.css';
import {ModalName} from "../../constants/structure";

import getUser from "../../actions/user/getUser";

interface GetSizesPanelProps {
    id: string;
    setActiveModal: Function;
    user: UserInfo;
}

const mapDispatchToProps = {
    getUser
}

const storeEnhancer = connect(null, mapDispatchToProps)

const GetSizesPanel = (props: GetSizesPanelProps) => {
    const [userHeight, setUserHeight] = React.useState(0);
    const handleUploadPhoto = () => {
        let data = new FormData();
        let data2 = new FormData();
        let imagedata = document.querySelector('#front_photo').files[0];
        let imagedata2 = document.querySelector('#side_photo').files[0];
        data.append("data", imagedata);
        data2.append("data", imagedata2)

        fetch(`${process.env.API_HOST}`, {
            mode: 'no-cors',
            method: "POST",
            body: {
                UserId: props.user.id,
                PhotoFront: data,
                PhotoSide: data2,
                Height: userHeight
            }
        }).then(function (res) {
            if (res.ok) {
                alert("Perfect! ");
            } else if (res.status == 401) {
                alert("Oops! ");
            }
        }, function (e) {
            alert("Error submitting form!");
        });
    }

    return (
        <Panel id={props.id}>
            <PanelHeader>
                Примерочная
            </PanelHeader>
            <Div>
                <Text weight="regular">
                    Загрузите две фотографии в полный рост: в профиль и в анфас
                </Text>
            </Div>
            <Div>
                <div className="sizeImages">
                    <img src={frontSize} alt="frontSize" className="sizeImage"/>
                    <img src={sideSize} alt="sideSize" className="sizeImage" />
                </div>
            </Div>
            <Group>
                <Cell expandable before={<Icon28HelpOutline />} onClick={() => { props.setActiveModal(ModalName.HelpSizes) }}>
                    <Link>Как улучшить результаты</Link>
                </Cell>
            </Group>
            <Div>
                <form encType="multipart/form-data">
                    <div className="uploadSizePhoto">
                        <FormLayoutGroup top="Ваш рост">
                            <Input type="text" onChange={setUserHeight} value={userHeight} />
                        </FormLayoutGroup>
                        <File top="Загрузите ваше фото" before={<Icon24Camera />} controlSize="l" id='front_photo'>
                            Выбрать фото в анфас
                        </File>
                        <File top="Загрузите ваше фото" before={<Icon24Camera />} controlSize="l" id='side_photo'>
                            Выбрать Фото в профиль
                        </File>
                        <Button onClick={handleUploadPhoto}>
                            Загрузить
                        </Button>
                    </div>
                </form>
            </Div>
        </Panel>
    )
}

export default storeEnhancer(GetSizesPanel);
