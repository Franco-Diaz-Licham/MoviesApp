import api from "../axios";

const ep: string = "/movie";
export const getGenres = () => api.get(ep);
export const getGenresById = (id: number) => api.get(`${ep}/${id}`);