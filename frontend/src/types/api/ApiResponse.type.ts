
/** Generic API wrapper from server, handle a list of responses. */
export interface ApiResponse<T> {
    statusCode: number;
    message: string;
    data: T[]
}

/** API response wrapper, handles a singular data response. */
export interface ApiSingleResponse<T> {
    statusCode: number;
    message: string;
    data: T
}