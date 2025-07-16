import { PhotoCreate } from "./PhotoCreate.type";

export interface PhotoUpdate extends PhotoCreate {
    id: number;
}
