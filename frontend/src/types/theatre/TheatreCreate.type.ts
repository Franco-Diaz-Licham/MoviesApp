import CoordinateDTO from "./CoordinateDTO.type";

export interface TheatreCreate extends CoordinateDTO {
    name: string;
    address: string;
}