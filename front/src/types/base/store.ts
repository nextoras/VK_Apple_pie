import { IDispatchedNotification } from '../notifications/notifications';
import ILoaderState from '../loader/store';
import IPopupState from '../popup/store';
import IErrorState from '../error/store';
import ICardsState from '../cards/store';

interface IGlobalState {
    notifications: IDispatchedNotification[];
    loader: ILoaderState;
    popup: IPopupState;
    error: IErrorState;
    cards: ICardsState;
}

export { IGlobalState };
