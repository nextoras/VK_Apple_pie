import React, { useState, useEffect } from 'react';
import bridge from '@vkontakte/vk-bridge';
import { Root } from "@vkontakte/vkui";
import View from '@vkontakte/vkui/dist/components/View/View';
import ScreenSpinner from '@vkontakte/vkui/dist/components/ScreenSpinner/ScreenSpinner';
import '@vkontakte/vkui/dist/vkui.css';

import {Views, PanelsByView, ViewName, PanelName, ModalName} from "./constants/structure";

import { Modal } from './Views/OnBoarding/Modal';
import SelectCloth from "./Views/OnBoarding/SelectCloth";

import Home from './panels/Home/Home';
import Persik from './panels/Persik/Persik';

const App = () => {
    const [activeView, setActiveView] = useState(ViewName.OnBoarding)
    const [activeModal, setActiveModal] = useState(ModalName.SelectCloth)
    const [activePanel, setActivePanel] = useState(PanelsByView[ViewName.OnBoarding][0]);
    const [fetchedUser, setUser] = useState(null);
    const [popout, setPopout] = useState(<ScreenSpinner size="large" />);

    useEffect(() => {
        bridge.subscribe(({ detail: { type, data } }) => {
            if (type === 'VKWebAppUpdateConfig') {
                const schemeAttribute = document.createAttribute('scheme');

                schemeAttribute.value = data.scheme ? data.scheme : 'client_light';
                document.body.attributes.setNamedItem(schemeAttribute);
            }
        });
        async function fetchData() {
            const user = await bridge.send('VKWebAppGetUserInfo');

            setUser(user);
            setPopout(null);
        }
        // fetchData();
    }, []);

    const go = e => {
        console.log('go')
        setActivePanel(e.currentTarget.dataset.to);
    };

    return (
        <Root activeView={activeView}>
            <View
                activePanel={activePanel}
                popout={popout}
                modal={
                    Modal({
                        activeModal,
                        handleNext: (view, panel, modal) => {
                            setActiveModal(modal);
                            setActivePanel(panel);
                            setActiveView(view);
                        }
                    })}
            >
                <SelectCloth id={PanelName.SelectCloth} />
            </View>
            <View activePanel={activePanel} popout={popout} modal={Modal(activeModal)}>
                <Home id fetchedUser={fetchedUser} go={go} />
                <Persik id="persik" go={go} />
            </View>
        </Root>
    );
};

export default App;
