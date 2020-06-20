import { combineReducers } from 'redux';
import notifications from './notifications';
import loader from './loader';
import popup from './popup';
import error from './error';

export default combineReducers({
    notifications,
    loader,
    popup,
    error,
});
