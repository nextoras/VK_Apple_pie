import * as React from 'react';

import { Panel, PanelHeader, Group, Cell, Div, Text, Link, File } from '@vkontakte/vkui';
import Icon28HelpOutline from '@vkontakte/icons/dist/28/help_outline';
import Icon24AddOutline from '@vkontakte/icons/dist/24/add_outline';

import frontSize from '../../assets/frontSize.png';
import sideSize from '../../assets/sideSize.png';

import './GetSizes.css';
import {ModalName} from "../../constants/structure";

interface GetSizesPanelProps {
    id: string,
    setActiveModal: Function
}

const GetSizesPanel = (props: GetSizesPanelProps) => {
    console.log('qwertyui')
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
                <div className="uploadSizePhoto">
                    <File before={<Icon24AddOutline />} controlSize="l">
                        Загрузить
                    </File>
                </div>
            </Div>
        </Panel>
    )
}

export default GetSizesPanel;
