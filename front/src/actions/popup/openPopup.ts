import { Popup } from '../../types/popup/actions';

const openPopup = payload => {
    return {
        payload,
        type: Popup.OPEN_POPUP,
    };
};

export default openPopup;
