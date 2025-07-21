import { PhotoCreate } from "../photo/PhotoCreate.type";

/** Movie create DTO. */
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
