// interfaces
import { Notification } from '../../types/notifications/actions';
import { IDispatchedNotification } from '../../types/notifications/notifications';

const addNotification = ({
    message,
    type,
    id,
    title,
    timer,
    onHide,
}: IDispatchedNotification) => ({
    type: Notification.ADD,
    payload: {
        id,
        type,
        title,
        timer,
        onHide,
        children: message,
        key: id,
    },
});

export default addNotification;
