import CoordinateDTO from "./CoordinateDTO.type";

export interface TheatreFormData extends CoordinateDTO  {
    id?: number;
    name: string;
    address: string;
}
