import { GenreCreate } from "./GenreCreate.type";

/** Genre update DTO. */
export interface GenreUpdate extends GenreCreate {
    id: number;
}
