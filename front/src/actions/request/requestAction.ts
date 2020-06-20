// services
import requestService from '../../services/request';

// actions
import showMessage from '../../actions/notification/showNotification';
import addError from '../../actions/error/addError';
import removeError from '../../actions/error/removeError';

// types
import {
    NotificationTypes,
    NotificationTitles,
} from '../../types/notifications/notifications';

const requestAction = ({ method, payload, errorType, ...rest }: any) => dispatch => {
    return requestService({
        method,
        payload,
        ...rest,
    })
        .then(res => res.json())
        .then(result => {
            dispatch(removeError());

            return result;
        })
        .catch(error => {
            dispatch(
                showMessage({
                    message: error.message,
                    type: NotificationTypes.error,
                    title: NotificationTitles.request_error,
                }),
            );
            dispatch(addError(error.message || 'There was an error'));

            return Promise.reject(error);
        });
};

export default requestAction;
