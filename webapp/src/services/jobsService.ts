import axios from 'axios';

const apiClient = axios.create({
  baseURL: 'https://localhost:7246/api', // Replace with your WebAPIHost base URL
  headers: {
    'Content-Type': 'application/json',
  },
});

export default {
  async getAllJobs() {
    const response = await apiClient.get('/Api');
    return response.data;
  },
};
