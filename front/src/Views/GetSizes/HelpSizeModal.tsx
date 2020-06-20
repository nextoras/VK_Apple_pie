import * as React from 'react';
import {Div, ModalPage, ModalPageHeader, ModalRoot, Text} from "@vkontakte/vkui";
import {ModalName, PanelName, ViewName} from "../../constants/structure";

interface HelpSizeModalProps {
    activeModal: string,
    modalBack: Function
}

const HelpSizeModal = ({activeModal, modalBack}: HelpSizeModalProps) => {
    return (
        <ModalRoot
            activeModal={activeModal}
            onClose={modalBack}
        >
            <ModalPage
                id={ModalName.HelpSizes}
                onClose={modalBack}
                header={
                    <ModalPageHeader>
                        Как улучшить результаты
                    </ModalPageHeader>
                }
            >
                <Div>
                    <Text weight="regular">
                        Для лучших результатов нужно встать с вытянутными руками, желательно при хорошем освещении
                    </Text>
                    <Text weight="regular">
                        Одежда должны быть облегающей
                    </Text>
                </Div>
            </ModalPage>
        </ModalRoot>
    )
}

export default HelpSizeModal;
