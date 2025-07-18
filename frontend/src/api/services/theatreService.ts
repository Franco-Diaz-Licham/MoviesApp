import { ApiResponse, ApiSingleResponse } from "../../types/ApiResponse.type";
import { TheatreCreate } from "../../types/theatre/TheatreCreate.type";
import { TheatreResponse } from "../../types/theatre/TheatreResponse.type";
import { TheatreUpdate } from "../../types/theatre/TheatreUpdate.type";
import api from "../axios";

/** Enpoint for current model. */
const ep: string = "/theatre";

/** Gets all Theatres. */
export const getTheatres = async (): Promise<TheatreResponse[] | null> => {
    try {
        const resp = await api.get<ApiResponse<TheatreResponse>>(ep);
        return resp.data.data;
    } catch (error: any) {
        console.error("Failed to fetch actors:", error);
        return null;
    }
};

/** Gets a Theatre by id. */
export const getTheatresById = async (id: number): Promise<TheatreResponse | null> => {
    try {
        const resp = await api.get<ApiSingleResponse<TheatreResponse>>(`${ep}/${id}`);
        return resp.data.data;
    } catch (error: any) {
        console.error("Failed to fetch Theatres:", error);
        return null;
    }
};

/** Creates a Theatre. */
export const createTheatre = async (model: TheatreCreate): Promise<TheatreResponse | null> => {
    try {
        const resp = await api.post<ApiSingleResponse<TheatreResponse>>(ep, model);
        return resp.data.data;
    } catch (error: any) {
        console.error("Failed to fetch Theatres:", error);
        return null;
    }
};

/** Updates a Theatre. */
export const updateTheatre = async (model: TheatreUpdate): Promise<TheatreResponse | null> => {
    try {
        const resp = await api.put<ApiSingleResponse<TheatreResponse>>(`${ep}/${model.id}`, model);
        return resp.data.data;
    } catch (error: any) {
        console.error("Failed to fetch Theatres:", error);
        return null;
    }
};

/** Deletes a Theatre. */
export const deleteTheatre = async (id: number): Promise<boolean> => {
    try {
        const resp = await api.delete(`${ep}/${id}`);
        return true;
    } catch (error: any) {
        console.error("Failed to fetch Theatres:", error);
        return false;
    }
};
