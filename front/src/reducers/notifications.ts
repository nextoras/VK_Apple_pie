// actions
import { Notification } from '../types/notifications/actions';

// types
import { INotificationStoreState } from '../types/notifications/notifications';

const initialState: INotificationStoreState = [];

const notifications = (state = initialState, action) => {
    switch (action.type) {
        case Notification.DELETE:
            return state.filter(item => item.id !== action.id);
        case Notification.ADD:
            const notification = {
                ...action.payload,
            };

            return [...state, notification];
        default:
            return state;
    }
};

export default notifications;
