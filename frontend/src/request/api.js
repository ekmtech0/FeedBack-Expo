import axios from "axios";

const api = axios.create({
  baseURL: import.meta.env.DEV 
    ? "http://localhost:5079/api"   // ambiente de desenvolvimento
    : "/api"                        // ambiente de produção
});

export default api;
