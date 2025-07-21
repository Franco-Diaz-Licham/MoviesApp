import { TheatreCreate } from "../types/theatre/TheatreCreate.type";
import { TheatreFormData } from "../types/theatre/TheatreFormData.type";
import { TheatreResponse } from "../types/theatre/TheatreResponse.type";
import { TheatreUpdate } from "../types/theatre/TheatreUpdate.type";

/** Maps from API response to form data DTO. */
export function mapResponseToForm(data: TheatreResponse): TheatreFormData {
    return {
        id: data.id,
        name: data.name,
        longitude: data.longitude,
        latitude: data.latitude,
        address: data.address
    };
}

/** Maps from form data DTO to create DTO. */
export function mapFormToCreate(data: TheatreFormData): TheatreCreate {
    return {
        name: data.name,
        longitude: data.longitude,
        latitude: data.latitude,
        address: data.address
    };
}

/** Maps from form data DTO to update DTO. */
export function mapFormToUpdate(data: TheatreFormData): TheatreUpdate {
    return {
        id: data.id!,
        name: data.name,
        longitude: data.longitude,
        latitude: data.latitude,
        address: data.address
    };
}
