import { MovieModel } from "../movie/MovieModel.type";

export interface GenreResponse {
    id: number;
    name: string;
    description: string;
    movies?: MovieModel[];
}
