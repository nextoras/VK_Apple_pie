import requestAction from "../request/requestAction";

const initUser = (userId, firstName, lastName, sexId) => dispatch => dispatch(
    requestAction({
        method: `/init?user_id=${userId}&firstName=${firstName}&lastName=${lastName}&sexId=${sexId}`
    })
)
    .then(res => {
        return true;
    })
    .catch(() => { return false; })

export default initUser;
