import { Error } from '../../types/error/actions';

const addError = (message: string) => ({
    type: Error.ADD_ERROR,
    payload: message,
});

export default addError;
