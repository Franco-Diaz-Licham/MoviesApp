import CoordinateDTO from "./CoordinateDTO.type";

export interface TheatreModel extends CoordinateDTO {
    name: string;
    address: string;
}