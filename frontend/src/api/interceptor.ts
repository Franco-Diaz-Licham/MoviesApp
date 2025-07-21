import { ApiResponseError } from "../types/api/ApiResponseError";
import api from "./axios";
import { AxiosError, AxiosInstance } from "axios";

interface SetupOptions {
    setLoading: (val: boolean) => void;
    show: (msg: string, options?: { result?: string }) => void;
}

/** Theoretoca delay used for development */
const delay = (ms: number) => new Promise((resolve) => setTimeout(resolve, ms));

/** Setup HTTP interceptor. */
export const setupInterceptors = (api: AxiosInstance, { setLoading, show }: SetupOptions) => {
    // Configure request
    api.interceptors.request.use(
        (config) => {
            if (config.url && !config.url.includes("account")) setLoading(true);
            const token = localStorage.getItem("token");
            if (token) config.headers.Authorization = `Bearer ${token}`;
            return config;
        },
        (error) => {
            setLoading(false);
            return Promise.reject(error);
        }
    );

    // Configure response
    api.interceptors.response.use(
        (response) => {
            const method = response.config.method?.toLowerCase();
            if (response.config.url && response.config.url.includes("login")) show("Login successful", { result: "success" });
            else if (response.config.url && response.config.url.includes("register")) show("Registration successful", { result: "success" });
            else if (method === "post" || method === "put") show("Saved successfully", { result: "success" });
            else if (method === "delete") show("Deleted successfully", { result: "warning" });
            setLoading(false);
            return response;
        },
        async (error: AxiosError<ApiResponseError>) => {
            await delay(1000);
            setLoading(false);
            var errors: string = "";
            if (error.response?.data.validationErrors) error.response.data.validationErrors.forEach((e) => (errors = errors + e));
            else errors = error.message;
            show(`Error: ${errors}`, { result: "danger" });
            return Promise.reject(error);
        }
    );
};

export default api;
