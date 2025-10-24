import axios from "axios";

export const http = axios.create({
  baseURL: 'http://localhost:5092/api', // URL base da API
  headers: {
    'Content-Type': 'application/json',
  },
});