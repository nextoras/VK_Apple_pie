import * as React from 'react';

import { Panel } from '@vkontakte/vkui';

import OnBoarding from '../../assets/Onboarding2.png'

interface SelectClothProps {
    id: string
}

const SizeCloth = (id: SelectClothProps) => {
    return (
        <Panel id={id}>
            <img src={OnBoarding} alt="OnBoarding size clothes"/>
        </Panel>
    )
}

export default SizeCloth;
