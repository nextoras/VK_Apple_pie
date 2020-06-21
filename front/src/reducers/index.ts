import { combineReducers } from 'redux';
import notifications from './notifications';
import loader from './loader';
import popup from './popup';
import error from './error';
import user from "./user";

export default combineReducers({
    notifications,
    loader,
    popup,
    error,
    user
});
