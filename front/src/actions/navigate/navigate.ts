// 3-rd party libs
import { History } from 'history';

// interfaces
import Navigation from '../../types/inner/navigation';

const navigate = (url: string, browserHistory: History) => {
    browserHistory.push(url);

    return {
        type: Navigation.NAVIGATE,
    };
};

export default navigate;
