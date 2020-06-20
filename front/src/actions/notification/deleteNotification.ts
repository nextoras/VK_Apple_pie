// interfaces
import { Notification } from '../../types/notifications/actions';

const deleteNotification = (id: string) => ({
    id,
    type: Notification.DELETE,
});

export default deleteNotification;
