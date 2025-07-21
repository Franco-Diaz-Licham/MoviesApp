import { PhotoCreate } from "./PhotoCreate.type";

/** Photo update DTO. */
export interface PhotoUpdate extends PhotoCreate {
    id: number;
}
