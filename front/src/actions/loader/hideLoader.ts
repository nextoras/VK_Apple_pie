import { Loader } from '../../types/loader/actions';
import { LoaderType } from '../../types/loader/loaderType';

const hideLoader = (loaderType: keyof typeof LoaderType = LoaderType.loadingPage) => ({
    type: Loader.HIDE_LOADER,
    payload: loaderType,
});

export default hideLoader;
