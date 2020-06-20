import * as React from 'react';
import { platform, IOS } from '@vkontakte/vkui';

import Panel from '@vkontakte/vkui/dist/components/Panel/Panel';
import PanelHeader from '@vkontakte/vkui/dist/components/PanelHeader/PanelHeader';
import PanelHeaderButton from '@vkontakte/vkui/dist/components/PanelHeaderButton/PanelHeaderButton';
import Icon28ChevronBack from '@vkontakte/icons/dist/28/chevron_back';
import Icon24Back from '@vkontakte/icons/dist/24/back';

import './Persik.css';

interface PersikProps {
    id: string;
    go: Function;
}

const osName = platform();

const Persik = (props: PersikProps) => (
    <Panel id={props.id}>
        <PanelHeader
            left={
                <PanelHeaderButton onClick={props.go} data-to="home">
                    {osName === IOS ? <Icon28ChevronBack /> : <Icon24Back />}
                </PanelHeaderButton>
            }
        >
            Persik
        </PanelHeader>
		<p>Persik here</p>
        <img className="Persik" alt="Persik The Cat" />
    </Panel>
);

export default Persik;
