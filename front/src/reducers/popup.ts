import { Popup } from '../types/popup/actions';

const initialState = {
    isOpen: false,
};

const popup = (state = initialState, { type, payload }) => {
    switch (type) {
        case Popup.OPEN_POPUP:
            return { payload, isOpen: true };
        case Popup.CLOSE_POPUP:
            return { isOpen: false };
        default:
            return state;
    }
};

export default popup;
