import GenreModel from "../../types/GenreModel.type";
import api from "../axios";

const ep: string = "/genre";
export const getGenres = () => api.get(ep);
export const getGenresById = (id: number) => api.get(`${ep}/${id}`);
export const createGenre = (model: GenreModel) => api.post(ep, model).catch((err) => console.error("Failed to fetch genres:", err));
export const updateGenre = (model: GenreModel) => api.post(`${ep}/${model.id}`, model).catch((err) => console.error("Failed to fetch genres:", err));
