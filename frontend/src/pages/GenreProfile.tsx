import { Params, useNavigate, useParams } from "react-router-dom";
import GenreForm from "../features/genre/GenreForm";
import GenreFormData from "../types/genre/GenreFormData.type";
import { createGenre, getGenresById, updateGenre } from "../api/services/genreService";
import { useEffect, useState } from "react";
import { mapFormToCreate, mapFormToUpdate, mapResponseToForm } from "../utils/GenreMapper";

/** Genre profile page. Allows editing and creating of new genres. */
export default function GenreProfile() {
    const [model, setModel] = useState<GenreFormData | null>(null);
    const { id }: Params<string> = useParams();
    const navigate = useNavigate();

    useEffect(() => {
        if (!id) return;
        fetchData(id);
    }, []);

    /** Gets data from the server. */
    const fetchData = async (id: string) => {
        const modelId = Number.parseInt(id);
        const resp = await getGenresById(modelId);
        if (resp) setModel(resp);
    };

    /** Handles the saving of data entry. */
    const handleOnSubmit = async (data: GenreFormData) => {
        if (id) return await update(data);
        await create(data);
    };

    /** Saves new entry to the Db. */
    const create = async (data: GenreFormData) => {
        var model = mapFormToCreate(data);
        var resp = await createGenre(model);
        var output = mapResponseToForm(resp!);
        setModel(output);
        navigate(`./${resp?.id}`);
    };

    /** Updates exsiting entry to the db. */
    const update = async (data: GenreFormData) => {
        var model = mapFormToUpdate(data);
        var resp = await updateGenre(model);
        var output = mapResponseToForm(resp!);
        setModel(output);
    };

    return <GenreForm model={model} onSubmit={handleOnSubmit} />;
}
