import { ActorCreate } from "../../types/actor/ActorCreate.type";
import { ActorResponse } from "../../types/actor/ActorResponse.type";
import { ActorUpdate } from "../../types/actor/ActorUpdate.type";
import { ApiResponse, ApiSingleResponse } from "../../types/api/ApiResponse.type";
import api from "../axios";

/** Enpoint for current model. */
const ep: string = "/actor";

/** Gets all actors. */
export const getActors = async (): Promise<ActorResponse[] | null> => {
    try {
        const resp = await api.get<ApiResponse<ActorResponse>>(ep);
        return resp.data.data;
    } catch (error: any) {
        console.error("Failed to fetch actors:", error);
        return null;
    }
};

/** Gets a actor by id. */
export const getActorsById = async (id: number): Promise<ActorResponse | null> => {
    try {
        const resp = await api.get<ApiSingleResponse<ActorResponse>>(`${ep}/${id}`);
        return resp.data.data;
    } catch (error: any) {
        console.error("Failed to fetch actors:", error);
        return null;
    }
};

/** Creates an actor. */
export const createActor = async (model: ActorCreate): Promise<ActorResponse | null> => {
    try {
        const formData = new FormData();
        formData.append("name", model.name);
        formData.append("dob", new Date(model.dob).toDateString());
        formData.append("biography", model.biography);
        formData.append("photo.image", model.photo.image[0]);

        const resp = await api.post<ApiSingleResponse<ActorResponse>>(ep, formData, {
            headers: { "Content-Type": "multipart/form-data" },
        });

        return resp.data.data;
    } catch (error: any) {
        console.error("Failed to create actor:", error);
        return null;
    }
};

/** Updates an actor. */
export const updateActor = async (model: ActorUpdate): Promise<ActorResponse | null> => {
    try {
        const formData = new FormData();
        formData.append("id", model.id.toString());
        formData.append("name", model.name);
        formData.append("dob", new Date(model.dob).toDateString());
        formData.append("biography", model.biography);
        
        if (model.photo) formData.append("photo.image", model.photo.image[0]);
        const resp = await api.put<ApiSingleResponse<ActorResponse>>(`${ep}/${model.id}`, formData, {
            headers: { "Content-Type": "multipart/form-data" },
        });

        return resp.data.data;
    } catch (error: any) {
        console.error("Failed to fetch actors:", error);
        return null;
    }
};

/** Deletes an actor. */
export const deleteActor = async (id: number): Promise<boolean> => {
    try {
        const resp = await api.delete(`${ep}/${id}`);
        return true;
    } catch (error: any) {
        console.error("Failed to fetch actors:", error);
        return false;
    }
};
