import * as React from 'react';

import { Panel } from '@vkontakte/vkui';

import OnBoarding from '../../assets/Onboarding3.png'

import style from './OnBoarding.css';

interface BuyClothProps {
    id: string;
}

const BuyCloth = (props: BuyClothProps) => {
    return (
        <Panel id={props.id}>
            <img src={OnBoarding} alt="OnBoarding buy clothes" className={style.onBoardingImage} />
        </Panel>
    )
}

export default BuyCloth;
