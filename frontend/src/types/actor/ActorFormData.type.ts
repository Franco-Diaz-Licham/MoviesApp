export interface ActorFormData {
    id?: number;
    name: string;
    dob: Date;
    biography: string;
    imgeUrl?: string;
    image: FileList | null;
}
