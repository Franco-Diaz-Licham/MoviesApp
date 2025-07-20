import { PhotoCreate } from "../photo/PhotoCreate.type";
import { MovieCreate } from "./MovieCreate.type";

export interface MovieUpdate {
    id: number;
    title: string;
    plot: string;
    inTheatresFlag: boolean;
    upComingFlag: boolean;
    photo: PhotoCreate | null;
    actors?: number[];
    genres?: number[];
    theatres?: number[];
}
