import { ActorResponse } from "../actor/ActorResponse.type";
import { GenreResponse } from "../genre/GenreResponse.type";
import { PhotoResponse } from "../photo/PhotoResponse.type";
import { TheatreResponse } from "../theatre/TheatreResponse.type";

/** Movie api response DTO. */
export interface MovieResponse {
    id: number;
    title: string;
    plot: string;
    inTheatresFlag: boolean;
    upComingFlag: boolean;
    photo: PhotoResponse;
    actors?: ActorResponse[];
    genres?: GenreResponse[];
    theatres?: TheatreResponse[];
}
