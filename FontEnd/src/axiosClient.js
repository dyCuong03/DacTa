import axios from 'axios';
import router from './router';

const axiosClient = axios.create({
  baseURL: 'https://localhost:44382/api/', 
});
export default axiosClient;