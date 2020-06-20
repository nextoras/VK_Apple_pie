import * as React from 'react';

import { ModalRoot, ModalCard } from "@vkontakte/vkui";
import {ModalName, PanelName, ViewName} from '../../constants/structure';

interface ModalProps {
    activeModal: ModalName;
    handleNext: Function
}

export const Modal = (props: ModalProps) => {
    const [activeModal, setActiveModal] = React.useState(props.activeModal);
    const [modalHistory, setModalHistory] = React.useState([]);

    const modalBack = () => {
        setActiveModal(modalHistory[modalHistory.length - 2])
    }

    return (
        <ModalRoot
            activeModal={activeModal}
            onClose={modalBack}
        >
            <ModalCard
                id={ModalName.SelectCloth}
                onClose={() => this.setActiveModal(null)}
                header="Выбирайте одежду которая подойдет к вашей фигуре"
                caption="Нейросеть вычислит размер вашей фигуры, вам останется выбрать одежду по вкусу"
                actions={[{
                    title: 'Дальше >',
                    mode: 'secondary',
                    action: () => {props.handleNext(ViewName.OnBoarding, PanelName.SizeCloth, ModalName.SizeCloth)}
                }]}
            />
            <ModalCard
                id={ModalName.SelectCloth}
                onClose={() => this.setActiveModal(null)}
                header="Примеряйте одежду на себя"
                caption="Почти как в настоящей примерочной, посмотрите как вещь смотрится на вас"
                actions={[{
                    title: 'Дальше >',
                    mode: 'secondary',
                    action: () => {props.handleNext(ViewName.OnBoarding, PanelName.BuyCloth, ModalName.BuyCloth)}
                }]}
            />
            <ModalCard
                id={ModalName.SelectCloth}
                onClose={() => this.setActiveModal(null)}
                header="Если вам все понравилось, смело покупайте в магазине"
                caption="Переходите в магазины партнеров, и договаривайтесь о доставке"
                actions={[{
                    title: 'Попробовать',
                    mode: 'primary',
                    action: () => {props.handleNext(ViewName.GetSizes, PanelName.Upload, null)}
                }]}
            />
        </ModalRoot>
    )
};
