import axios from 'axios';
import router from '../../router';

const api = axios.create({
    baseURL: 'http://localhost:5059',
});

api.interceptors.request.use(request => {
    if(localStorage.getItem('jwt')) {
        const token = localStorage.getItem('jwt');
        request.headers!.Authorization = `${token}`;
    }

    return request;
});

api.interceptors.response.use(response => response, error => {
    if(error.response.status === 401) {
        localStorage.clear();
        router.push('/login');
    }
})

export default api;