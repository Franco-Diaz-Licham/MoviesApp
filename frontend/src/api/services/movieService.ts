import { ApiResponse, ApiSingleResponse } from "../../types/api/ApiResponse.type";
import { MovieCreate } from "../../types/movie/MovieCreate.type";
import { MovieResponse } from "../../types/movie/MovieResponse.type";
import { MovieUpdate } from "../../types/movie/MovieUpdate.type";
import api from "../axios";

/** Enpoint for current model. */
const ep: string = "/movie";

/** Gets all Movies. */
export const getMovies = async (): Promise<MovieResponse[] | null> => {
    try {
        const resp = await api.get<ApiResponse<MovieResponse>>(ep);
        return resp.data.data;
    } catch (error: any) {
        console.error("Failed to fetch Movies:", error);
        return null;
    }
};

/** Gets a Movie by id. */
export const getMoviesById = async (id: number): Promise<MovieResponse | null> => {
    try {
        const resp = await api.get<ApiSingleResponse<MovieResponse>>(`${ep}/${id}`);
        return resp.data.data;
    } catch (error: any) {
        console.error("Failed to fetch Movies:", error);
        return null;
    }
};

/** Creates an Movie. */
export const createMovie = async (model: MovieCreate): Promise<MovieResponse | null> => {
    try {
        const formData = new FormData();
        formData.append("title", model.title);
        formData.append("plot", model.plot);
        formData.append("inTheatresFlag", String(model.inTheatresFlag));
        formData.append("upComingFlag", String(model.upComingFlag));
        model.actors?.forEach((id) => formData.append("actors", id.toString()));
        model.genres?.forEach((id) => formData.append("genres", id.toString()));
        model.theatres?.forEach((id) => formData.append("theatres", id.toString()));
        formData.append("photo.image", model.photo.image[0]);

        const resp = await api.post<ApiSingleResponse<MovieResponse>>(ep, formData, {
            headers: { "Content-Type": "multipart/form-data" },
        });

        return resp.data.data;
    } catch (error: any) {
        console.error("Failed to create Movie:", error);
        return null;
    }
};

/** Updates an Movie. */
export const updateMovie = async (model: MovieUpdate): Promise<MovieResponse | null> => {
    try {
        const formData = new FormData();
        formData.append("id", model.id.toString());
        formData.append("title", model.title);
        formData.append("plot", model.plot);
        formData.append("inTheatresFlag", String(model.inTheatresFlag));
        formData.append("upComingFlag", String(model.upComingFlag));
        model.actors?.forEach((id) => formData.append("actors", id.toString()));
        model.genres?.forEach((id) => formData.append("genres", id.toString()));
        model.theatres?.forEach((id) => formData.append("theatres", id.toString()));
        if (model.photo) formData.append("photo.image", model.photo.image[0]);
        
        const resp = await api.put<ApiSingleResponse<MovieResponse>>(`${ep}/${model.id}`, formData, {
            headers: { "Content-Type": "multipart/form-data" },
        });

        return resp.data.data;
    } catch (error: any) {
        console.error("Failed to fetch Movies:", error);
        return null;
    }
};

/** Deletes an Movie. */
export const deleteMovie = async (id: number): Promise<boolean> => {
    try {
        const resp = await api.delete(`${ep}/${id}`);
        return true;
    } catch (error: any) {
        console.error("Failed to fetch Movies:", error);
        return false;
    }
};
