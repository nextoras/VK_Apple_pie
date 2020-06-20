import * as React from 'react';

import { ModalRoot, ModalCard } from "@vkontakte/vkui";
import {ModalName, PanelName, ViewName} from '../../constants/structure';

interface ModalProps {
    activeModal: ModalName;
    handleNext: Function
}

export const Modal = ({activeModal, modalBack, handleNext}) => {
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
                    action: () => {handleNext(ViewName.OnBoarding, PanelName.SizeCloth, ModalName.SizeCloth)}
                }]}
            />
            <ModalCard
                id={ModalName.SizeCloth}
                onClose={() => this.setActiveModal(null)}
                header="Примеряйте одежду на себя"
                caption="Почти как в настоящей примерочной, посмотрите как вещь смотрится на вас"
                actions={[{
                    title: 'Дальше >',
                    mode: 'secondary',
                    action: () => {handleNext(ViewName.OnBoarding, PanelName.BuyCloth, ModalName.BuyCloth)}
                }]}
            />
            <ModalCard
                id={ModalName.BuyCloth}
                onClose={() => this.setActiveModal(null)}
                header="Если вам все понравилось, смело покупайте в магазине"
                caption="Переходите в магазины партнеров, и договаривайтесь о доставке"
                actions={[{
                    title: 'Попробовать',
                    mode: 'primary',
                    action: () => {handleNext(ViewName.GetSizes, PanelName.Upload, null)}
                }]}
            />
        </ModalRoot>
    )
};
