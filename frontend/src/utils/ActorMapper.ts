import { ActorResponse } from "../types/actor/ActorResponse.type";
import { ActorCreate } from "../types/actor/ActorCreate.type";
import { ActorFormData } from "../types/actor/ActorFormData.type";
import { ActorUpdate } from "../types/actor/ActorUpdate.type";
import { PhotoCreate } from "../types/photo/PhotoCreate.type";

/** Maps from API response to form data DTO. */
export function mapResponseToForm(data: ActorResponse): ActorFormData {
    return {
        id: data.id,
        name: data.name,
        dob: data.dob,
        biography: data.biography,
        image: null,
        imgeUrl: data.photo.publicUrl,
    };
}

/** Maps from form data DTO to create DTO. */
export function mapFormToCreate(data: ActorFormData): ActorCreate {
    if (data.image?.length === 0) throw new Error("There is no image!");
    return {
        name: data.name,
        dob: data.dob,
        biography: data.biography,
        photo: {
            image: Array.from(data.image!),
        },
    };
}

/** Maps from form data DTO to update DTO. */
export function mapFormToUpdate(data: ActorFormData): ActorUpdate {
    let image: PhotoCreate | null =
        data.image && data.image.length > 0
            ? {
                  image: Array.from(data.image),
              }
            : null;

    return {
        id: data.id!,
        name: data.name,
        dob: data.dob,
        biography: data.biography,
        photo: image,
    };
}
