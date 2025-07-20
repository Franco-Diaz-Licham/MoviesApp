import { ActorResponse } from "../types/actor/ActorResponse.type";
import { ActorCreate } from "../types/actor/ActorCreate.type";
import { ActorFormData } from "../types/actor/ActorFormData.type";
import { ActorUpdate } from "../types/actor/ActorUpdate.type";
import { error } from "console";
import { PhotoCreate } from "../types/photo/PhotoCreate.type";

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
