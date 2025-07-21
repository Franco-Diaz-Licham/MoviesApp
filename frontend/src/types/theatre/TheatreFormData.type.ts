import CoordinateDTO from "./CoordinateDTO.type";

/** Form data for theatres. */
export interface TheatreFormData extends CoordinateDTO  {
    id?: number;
    name: string;
    address: string;
}
