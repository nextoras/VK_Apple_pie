const defaultNotificationTimer = 10000;

// actions
import addNotification from './addNotification';
import deleteNotification from './deleteNotification';

// types
import { IBasicNotification } from '../../types/notifications/notifications';

function generateNotificationId(): string {
    return Math.random()
        .toString(36)
        .substr(2, 9);
}

function onHide(dispatch, id: string): any {
    return () => {
        dispatch(deleteNotification(id));
    };
}

const showNotification = ({ message, type, title }: IBasicNotification) => {
    return dispatch => {
        const id = generateNotificationId();

        dispatch(
            addNotification({
                message,
                type,
                id,
                title,
                timer: defaultNotificationTimer,
                onHide: onHide(dispatch, id),
            }),
        );
    };
};

export default showNotification;
