import axios from 'axios';
import router from './router';

const axiosClient = axios.create({
  baseURL: 'https://localhost:7179/api/', 
});
export default axiosClient;