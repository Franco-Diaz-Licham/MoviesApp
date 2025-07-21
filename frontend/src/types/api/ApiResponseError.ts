
export interface ApiResponseError {
    validationErrors?: string[];
    statusCode: number;
    message: string;
}