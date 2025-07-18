import CoordinateDTO from "./CoordinateDTO.type";

export interface TheatreResponse extends CoordinateDTO {
    id: number;
    name: string;
    address: string;
}