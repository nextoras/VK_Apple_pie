import { Loader } from '../types/loader/actions';
import ILoaderState from '../types/loader/store';
import { LoaderType } from '../types/loader/loaderType';

const initialState: ILoaderState = {};

type LoaderAction = {
    type: string;
    payload: keyof typeof LoaderType;
};

const loader = (state = initialState, { type, payload: loaderType }: LoaderAction) => {
    switch (type) {
        case Loader.SHOW_LOADER:
            return { [loaderType]: true };
        case Loader.HIDE_LOADER:
            return { [loaderType]: false };
        default:
            return state;
    }
};

export default loader;
