import { PhotoCreate } from "../photo/PhotoCreate.type";

/** Actor create DTO. */
export interface ActorCreate {
    name: string;
    dob: Date;
    photo: PhotoCreate;
    biography: string;
}
