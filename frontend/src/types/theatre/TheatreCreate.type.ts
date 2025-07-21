import CoordinateDTO from "./CoordinateDTO.type";

/** Theatre create DTO. */
export interface TheatreCreate extends CoordinateDTO {
    name: string;
    address: string;
}