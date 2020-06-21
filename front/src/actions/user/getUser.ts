import requestAction from "../request/requestAction";
import {UserEvent} from "../../reducers/user";

const getUser = (userId) => dispatch => dispatch(
    requestAction({
        method: `/get_user?userId=${userId}`
    })
)
    .then(res => {
        dispatch({
            type: UserEvent.Load,
            payload: res
        });

        return true;
    })
    .catch(() => { return false; })

export default getUser;
