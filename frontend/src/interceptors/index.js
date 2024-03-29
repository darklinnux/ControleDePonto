import axios from 'axios';
import env from 'env-var';

function configRequest() {
  axios.defaults.timeout = 15000;
  axios.defaults.baseURL = env.get('REACT_APP_BACKEND_URL').required().asUrlString();
  const token = localStorage.getItem('token');
  if (token) {
    axios.defaults.headers.common = {'Authorization': `Bearer ${token}`}
    return axios;
  } else {
    return axios;
  }
}
const req = configRequest()
export { req }
