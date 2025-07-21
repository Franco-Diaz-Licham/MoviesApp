import { PhotoResponse } from "../photo/PhotoResponse.type";

/** Main response DTO. */
export interface ActorResponse {
    id: number;
    name: string;
    dob: Date;
    photo: PhotoResponse;
    biography: string;
}
