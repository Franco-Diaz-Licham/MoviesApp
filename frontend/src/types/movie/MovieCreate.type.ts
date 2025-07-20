import { PhotoCreate } from "../photo/PhotoCreate.type";

export interface MovieCreate {
    title: string;
    plot: string;
    inTheatresFlag: boolean;
    upComingFlag: boolean;
    photo: PhotoCreate;
    actors?: number[];
    genres?: number[];
    theatres?: number[];
}
