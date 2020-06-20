import * as React from 'react';

import { Panel } from '@vkontakte/vkui';

import OnBoarding from '../../assets/Onboarding1.png'

interface SelectClothProps {
    id: string
}

const SelectCloth = (id: SelectClothProps) => {
    return (
        <Panel id={id}>
            <img src={OnBoarding} alt="OnBoarding select clothes"/>
        </Panel>
    )
}

export default SelectCloth;
