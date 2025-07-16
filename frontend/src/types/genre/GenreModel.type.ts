import { MovieModel } from "../movie/MovieModel.type";

export default interface GenreModel {
    id: number;
    name: string;
    description: string;
    movies?: MovieModel[];
}
