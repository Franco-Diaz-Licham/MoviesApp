import api from "./axios";
import { AxiosInstance } from "axios";

interface SetupOptions {
    setLoading: (val: boolean) => void;
    show: (msg: string, options?: { result?: string }) => void;
}

const delay = (ms: number) => new Promise((resolve) => setTimeout(resolve, ms));

/** Setup HTTP interceptor. */
export const setupInterceptors = (api: AxiosInstance, { setLoading, show }: SetupOptions) => {
    // Configure request
    api.interceptors.request.use(
        (config) => {
            setLoading(true);
            return config;
        },
        (error) => {
            setLoading(false);
            show(`Error: ${error.message}`, { result: "danger" });
            return Promise.reject(error);
        }
    );

    // Configure response
    api.interceptors.response.use(
        (response) => {
            const method = response.config.method?.toLowerCase();
            if (method === "post" || method === "put") {
                show("Saved successfully", { result: "success" });
            }
            setLoading(false);
            return response;
        },
        async (error) => {
            await delay(1000);
            setLoading(false);
            show(`Error: ${error.message}`, { result: "danger" });
            return Promise.reject(error);
        }
    );
};

export default api;
