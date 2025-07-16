import { PhotoCreate } from "../photo/PhotoCreate.type";

export interface ActorCreate {
    name: string;
    dob: Date;
    photo: PhotoCreate;
    biography: string;
}
