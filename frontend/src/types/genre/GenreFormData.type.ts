import { MovieModel } from "../movie/MovieModel.type";

export default interface GenreFormData {
    id?: number;
    name: string;
    description: string;
    movies?: MovieModel[];
}
