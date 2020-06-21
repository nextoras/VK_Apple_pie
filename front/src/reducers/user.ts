export enum UserEvent {
    Load = 'LOAD_USER'
}

const initialState = {
    user: {},
    authorized: false
};

const user = (state = initialState, action) => {
    switch (action.type) {
        case UserEvent.Load:
            return {
                ...state,
                authorized: true
            }
        default:
            return state;
    }
};

export default user;
