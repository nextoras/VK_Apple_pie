import * as React from 'react';

import { Panel } from '@vkontakte/vkui';

import OnBoarding from '../../assets/Onboarding1.png'

import './OnBoarding.css';

interface SelectClothProps {
    id: string;
}

const SelectCloth = (props: SelectClothProps) => {
    return (
        <Panel id={props.id}>
            <img src={OnBoarding} alt="OnBoarding select clothes" className="onBoardingImage" />
        </Panel>
    )
}

export default SelectCloth;
