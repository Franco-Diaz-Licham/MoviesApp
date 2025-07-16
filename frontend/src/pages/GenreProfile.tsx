import { Params, useNavigate, useParams } from "react-router-dom";
import GenreForm from "../features/genre/GenreForm";
import GenreModel from "../types/genre/GenreModel.type";
import { createGenre, getGenresById, updateGenre } from "../api/services/genreService";
import { useEffect, useState } from "react";

/** Genre profile page. Allows editing and creating of new genres. */
export default function GenreProfile() {
    const [model, setModel] = useState<GenreModel | null>(null);
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
    const handleOnSubmit = async (data: GenreModel) => {
        if (id) return await updateGenre(data);
        const model = await createGenre(data);
        setModel(model);
        navigate(`./${model?.id}`);
    };

    return <GenreForm model={model} onSubmit={handleOnSubmit} />;
}
