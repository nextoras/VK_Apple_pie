const requestService = ({
    host = process.env.API_HOST,
    method,
    httpMethod = 'get',
    payload,
    type = 'json',
    headers = {},
    ...rest
}) => {
    return fetch(`${host}${method}`, {
        ...rest,
        method: httpMethod,
        headers,
    }).catch(error => Promise.reject(error));
};

export default requestService;
