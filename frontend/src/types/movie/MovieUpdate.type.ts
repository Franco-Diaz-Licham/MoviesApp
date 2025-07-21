import { PhotoCreate } from "../photo/PhotoCreate.type";

/** Movie update DTO. */
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
