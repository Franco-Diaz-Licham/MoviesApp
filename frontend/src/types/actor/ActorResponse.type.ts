import { PhotoResponse } from "../photo/PhotoResponse.type";

export interface ActorResponse {
    id: number;
    name: string;
    dob: Date;
    photo: PhotoResponse;
    biography: string;
}
