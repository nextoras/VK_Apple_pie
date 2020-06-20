import * as React from 'react';

import { Panel } from '@vkontakte/vkui';

import OnBoarding from '../../assets/Onboarding2.png'

import './OnBoarding.css';

interface SelectClothProps {
    id: string;
}

const SizeCloth = (props: SelectClothProps) => {
    return (
        <Panel id={props.id}>
            <img src={OnBoarding} alt="OnBoarding size clothes" className="onBoardingImage"/>
        </Panel>
    )
}

export default SizeCloth;
