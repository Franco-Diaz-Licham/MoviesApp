import { GenreCreate } from "../types/genre/GenreCreate.type";
import GenreFormData from "../types/genre/GenreFormData.type";
import { GenreResponse } from "../types/genre/GenreResponse.type";
import { GenreUpdate } from "../types/genre/GenreUpdate.type";

export function mapResponseToForm(data: GenreResponse): GenreFormData {
    return {
        id: data.id,
        name: data.name,
        description: data.description,
    };
}

export function mapFormToCreate(data: GenreFormData): GenreCreate {
    return {
        name: data.name,
        description: data.description,
    };
}

export function mapFormToUpdate(data: GenreFormData): GenreUpdate {
    return {
        id: data.id!,
        name: data.name,
        description: data.description,
    };
}
