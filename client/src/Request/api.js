import axios from "axios";

export const http = axios.create({
  baseURL: 'https://premio-nacional-imobiliario-jfnm.onrender.com/api', // URL base da API
  // baseURL: 'http://localhost:5092/api',
  headers: {
    'Content-Type': 'application/json',
  },
});