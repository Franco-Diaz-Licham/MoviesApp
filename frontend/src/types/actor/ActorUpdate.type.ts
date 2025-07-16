import { PhotoCreate } from "../photo/PhotoCreate.type";
import { ActorCreate } from "./ActorCreate.type";

export interface ActorUpdate {
    id: number;
    name: string;
    dob: Date;
    photo: PhotoCreate | null;
    biography: string;
}
