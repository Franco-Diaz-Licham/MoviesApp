import { ApiResponse, ApiSingleResponse } from "../../types/ApiResponse.type";
import GenreModel from "../../types/genre/GenreModel.type";
import api from "../axios";

/** Enpoint for current model. */
const ep: string = "/genre";

/** Gets all genres. */
export const getGenres = () => api.get(ep).then((resp) => resp.data);

/** Gets a genre by id. */
export const getGenresById = async (id: number): Promise<GenreModel | null> => {
    try {
        const resp = await api.get<ApiSingleResponse<GenreModel>>(`${ep}/${id}`);
        return resp.data.data;
    } catch (error: any) {
        console.error("Failed to fetch genres:", error);
        return null;
    }
};

/** Creates a genre. */
export const createGenre = async (model: GenreModel): Promise<GenreModel | null> => {
    try {
        const resp = await api.post<ApiSingleResponse<GenreModel>>(ep, model);
        return resp.data.data;
    } catch (error: any) {
        console.error("Failed to fetch genres:", error);
        return null;
    }
};

/** Updates a genre. */
export const updateGenre = async (model: GenreModel): Promise<GenreModel | null> => {
    try {
        const resp = await api.put<ApiSingleResponse<GenreModel>>(`${ep}/${model.id}`, model);
        return resp.data.data;
    } catch (error: any) {
        console.error("Failed to fetch genres:", error);
        return null;
    }
};

/** Deletes a genre. */
export const deleteGenre = async (id: number): Promise<boolean> => {
    try {
        const resp = await api.delete(`${ep}/${id}`);
        return true;
    } catch (error: any) {
        console.error("Failed to fetch genres:", error);
        return false;
    }
};
