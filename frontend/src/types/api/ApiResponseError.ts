
/** Api response error. Main contain validation error information. */
export interface ApiResponseError {
    validationErrors?: string[];
    statusCode: number;
    message: string;
}