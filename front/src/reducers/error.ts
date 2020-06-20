import { Error } from '../types/error/actions';

type ErrorAction = {
    type: string;
    payload: string;
};

const error = (state = null, { type, payload }: ErrorAction) => {
    switch (type) {
        case Error.ADD_ERROR:
            return payload;
        case Error.REMOVE_ERROR:
            return null;
        default:
            return state;
    }
};

export default error;
