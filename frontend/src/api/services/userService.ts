import { ApiSingleResponse } from "../../types/api/ApiResponse.type";
import { LoginFormData } from "../../types/user/LoginFormData.type";
import { UserResponse } from "../../types/user/UserResponse.type";
import { UserUpdate } from "../../types/user/UserUpdate.type";
import api from "../axios";

/** Enpoint for current model. */
const ep: string = "/account";

/** Gets all Users. */
export const registerUser = async (model: LoginFormData): Promise<UserResponse | null> => {
    try {
        const resp = await api.post<ApiSingleResponse<UserResponse>>(`${ep}/register`, model);
        let user = resp.data.data;
        if (resp.status && resp.data.data.token) localStorage.setItem("token", user.token);
        return user;
    } catch (error: any) {
        console.error("Failed to fetch actors:", error);
        return null;
    }
};

export const updateUser = async (model: UserUpdate): Promise<UserResponse | null> => {
    try {
        const resp = await api.put<ApiSingleResponse<UserResponse>>(ep, model);
        return resp.data.data;
    } catch (error: any) {
        console.error("Failed to fetch actors:", error);
        return null;
    }
};

export const emailCheck = async (email: string): Promise<UserResponse | null> => {
    try {
        let query = `${ep}/email?email=${encodeURIComponent(email)}`;
        const resp = await api.get<ApiSingleResponse<UserResponse>>(query);
        return resp.data.data;
    } catch (error: any) {
        console.error("Failed to fetch actors:", error);
        return null;
    }
};

export const loginUser = async (model: LoginFormData): Promise<UserResponse | null> => {
    try {
        const resp = await api.post<ApiSingleResponse<UserResponse>>(`${ep}/login`, model);
        let user = resp.data.data;
        if (resp.status && resp.data.data.token) localStorage.setItem("token", user.token);
        return user;
    } catch (error: any) {
        console.error("Failed to fetch actors:", error);
        return null;
    }
};

export const getUser = async (): Promise<UserResponse | null> => {
    try {
        const resp = await api.post<ApiSingleResponse<UserResponse>>(ep);
        return resp.data.data;
    } catch (error: any) {
        console.error("Failed to fetch actors:", error);
        return null;
    }
};
