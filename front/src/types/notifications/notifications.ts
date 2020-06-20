enum NotificationTypes {
    error = 'error',
    success = 'success',
    warning = 'warning',
}

enum NotificationTitles {
    request_error = 'Ошибка запроса',
}

interface IBasicNotification {
    type: NotificationTypes;
    message: string;
    title: string;
}

interface IDispatchedNotification extends IBasicNotification {
    id: string;
    timer: number;
    onHide: () => void;
}

interface INotificationStoreState extends Array<IDispatchedNotification> {}

export {
    IBasicNotification,
    NotificationTypes,
    NotificationTitles,
    IDispatchedNotification,
    INotificationStoreState,
};
