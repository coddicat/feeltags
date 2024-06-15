import axios from 'axios';
import router from '@/router/index';

const instance = axios.create({
  baseURL: './api'
});

instance.interceptors.response.use(
  response => {
    return response;
  },
  error => {
    if ([401, 403].includes(error.response.status)) {
      router.replace({ name: 'login' });
    }
    return Promise.reject(error);
  }
);

export default instance;
