import CoordinateDTO from "./CoordinateDTO.type";

/** Theatre response DTO. */
export interface TheatreResponse extends CoordinateDTO {
    id: number;
    name: string;
    address: string;
}