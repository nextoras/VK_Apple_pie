import { Error } from '../../types/error/actions';

const removeError = () => ({
    type: Error.REMOVE_ERROR,
});

export default removeError;
