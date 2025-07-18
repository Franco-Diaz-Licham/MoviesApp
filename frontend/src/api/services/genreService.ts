import { ApiResponse, ApiSingleResponse } from "../../types/ApiResponse.type";
import { GenreCreate } from "../../types/genre/GenreCreate.type";
import { GenreResponse } from "../../types/genre/GenreResponse.type";
import { GenreUpdate } from "../../types/genre/GenreUpdate.type";
import api from "../axios";

/** Enpoint for current model. */
const ep: string = "/genre";

/** Gets all genres. */
export const getGenres = async (): Promise<GenreResponse[] | null> => {
    try {
        const resp = await api.get<ApiResponse<GenreResponse>>(ep);
        return resp.data.data;
    } catch (error: any) {
        console.error("Failed to fetch actors:", error);
        return null;
    }
};

/** Gets a genre by id. */
export const getGenresById = async (id: number): Promise<GenreResponse | null> => {
    try {
        const resp = await api.get<ApiSingleResponse<GenreResponse>>(`${ep}/${id}`);
        return resp.data.data;
    } catch (error: any) {
        console.error("Failed to fetch genres:", error);
        return null;
    }
};

/** Creates a genre. */
export const createGenre = async (model: GenreCreate): Promise<GenreResponse | null> => {
    try {
        const resp = await api.post<ApiSingleResponse<GenreResponse>>(ep, model);
        return resp.data.data;
    } catch (error: any) {
        console.error("Failed to fetch genres:", error);
        return null;
    }
};

/** Updates a genre. */
export const updateGenre = async (model: GenreUpdate): Promise<GenreResponse | null> => {
    try {
        const resp = await api.put<ApiSingleResponse<GenreResponse>>(`${ep}/${model.id}`, model);
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
