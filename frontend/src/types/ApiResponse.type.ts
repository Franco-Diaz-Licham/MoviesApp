
export interface ApiResponse<T> {
    statusCode: number;
    message: string;
    data: T[]
}

export interface ApiSingleResponse<T> {
    statusCode: number;
    message: string;
    data: T
}