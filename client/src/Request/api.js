import axios from "axios";

const baseURL =
  import.meta.env.MODE === "production"
    ? "/api"                 // produção
    : "http://localhost:5092/api"; // desenvolvimento

export const http = axios.create({
  baseURL,
  headers: {
    "Content-Type": "application/json",
  },
  withCredentials: true, // necessário pros cookies HttpOnly
});
