
export interface MovieFormData {
    id?: number;
    title: string;
    plot: string;
    inTheatresFlag: boolean;
    upComingFlag: boolean;
    imgeUrl?: string;
    image: FileList | null;
    actors?: number[];
    genres?: number[];
    theatres?: number[];
}
