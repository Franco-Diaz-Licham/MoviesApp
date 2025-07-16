import { ActorResponse } from "../actor/ActorResponse.type";
import { BaseSelectoModel } from "../BaseSelectoModel.type";
import GenreModel from "../genre/GenreModel.type";
import { TheatreModel } from "../TheatreModel.type";

export interface MovieModel extends BaseSelectoModel {
    title: string;
    inTheatresFlag: boolean;
    upComingFlag: boolean;
    imageUrl?: string;
    actors?: ActorResponse[];
    genres?: GenreModel[];
    theatres?: TheatreModel[];
}