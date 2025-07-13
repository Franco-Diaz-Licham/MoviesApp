import api from "../axios";

const ep: string = "/actor";
export const getGenres = () => api.get(ep);
export const getGenresById = (id: number) => api.get(`${ep}/${id}`);