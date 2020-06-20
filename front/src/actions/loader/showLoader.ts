import { Loader } from '../../types/loader/actions';
import { LoaderType } from '../../types/loader/loaderType';

const showLoader = (loaderType: keyof typeof LoaderType = LoaderType.loadingPage) => {
    return {
        type: Loader.SHOW_LOADER,
        payload: loaderType,
    };
};

export default showLoader;
