import { PhotoCreate } from "../photo/PhotoCreate.type";

/** Update DTO for actors. */
export interface ActorUpdate {
    id: number;
    name: string;
    dob: Date;
    photo: PhotoCreate | null;
    biography: string;
}
