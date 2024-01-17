import axios from 'axios';


export const httpClient = axios.create({
    baseURL: "http://server:32333/api"
})