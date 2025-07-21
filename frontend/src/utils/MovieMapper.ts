import { MovieCreate } from "../types/movie/MovieCreate.type";
import { MovieFormData } from "../types/movie/MovieFormData.type";
import { MovieResponse } from "../types/movie/MovieResponse.type";
import { MovieUpdate } from "../types/movie/MovieUpdate.type";
import { PhotoCreate } from "../types/photo/PhotoCreate.type";

export function mapResponseToForm(data: MovieResponse): MovieFormData {
    return {
        id: data.id,
        title: data.title,
        plot: data.plot,
        inTheatresFlag: data.inTheatresFlag,
        upComingFlag: data.upComingFlag,
        image: null,
        imgeUrl: data.photo.publicUrl,
        genres: data.genres?.map(m => m.id),
        actors: data. actors?.map(m => m.id),
        theatres: data.theatres?.map(m => m.id)
    };
}

export function mapFormToCreate(data: MovieFormData): MovieCreate {
    if (data.image?.length === 0) throw new Error("There is no image!");
    return {
        title: data.title,
        plot: data.plot,
        inTheatresFlag: data.inTheatresFlag,
        upComingFlag: data.upComingFlag,
        photo: {
            image: Array.from(data.image!),
        },
        actors: data.actors,
        genres: data.genres,
        theatres: data.theatres
    };
}

export function mapFormToUpdate(data: MovieFormData): MovieUpdate {
    let image: PhotoCreate | null =
        data.image && data.image.length > 0
            ? {
                  image: Array.from(data.image),
              }
            : null;

    return {
        id: data.id!,
        title: data.title,
        plot: data.plot,
        inTheatresFlag: data.inTheatresFlag,
        upComingFlag: data.upComingFlag,
        photo: image,
        actors: data.actors,
        genres: data.genres,
        theatres: data.theatres
    };
}

