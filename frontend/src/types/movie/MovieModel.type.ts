import { ActorResponse } from "../actor/ActorResponse.type";
import { BaseSelectoModel } from "../BaseSelectoModel.type";
import GenreModel from "../genre/GenreFormData.type";
import { TheatreResponse } from "../theatre/TheatreResponse.type";

export interface MovieModel extends BaseSelectoModel {
    title: string;
    inTheatresFlag: boolean;
    upComingFlag: boolean;
    imageUrl?: string;
    actors?: ActorResponse[];
    genres?: GenreModel[];
    theatres?: TheatreResponse[];
}