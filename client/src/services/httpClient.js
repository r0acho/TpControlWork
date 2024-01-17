import axios from 'axios';


export const httpClient = axios.create({
    baseURL: "http://localhost:5248/api",
    headers: {
        "Content-Type": "application/json",
        "Access-Control-Allow-Origin": "*"
      }
})