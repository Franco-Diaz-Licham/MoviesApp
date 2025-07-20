import { useEffect, useState } from "react";
import { MovieFormData } from "../types/movie/MovieFormData.type";
import { Params, useNavigate, useParams } from "react-router-dom";
import { createMovie, getMoviesById, updateMovie } from "../api/services/movieService";
import { mapFormToCreate, mapFormToUpdate, mapResponseToForm } from "../utils/MovieMapper";
import MovieForm from "../features/movie/MovieForm";

export default function MovieProfile() {
    const [model, setModel] = useState<MovieFormData | null>(null);
    const { id }: Params<string> = useParams();
    const navigate = useNavigate();

    useEffect(() => {
        if (!id) return;
        fetchData(id);
    }, []);

    /** Gets data from the server. */
    const fetchData = async (id: string) => {
        const modelId = Number.parseInt(id);
        const resp = await getMoviesById(modelId);
        const model = mapResponseToForm(resp!);
        setModel(model);
    };

    /** Handles the saving of data entry. */
    const handleOnSubmit = async (data: MovieFormData) => {
        console.log(data);
        if (id) return await update(data);
        await create(data);
    };

    /** Saves new entry to the Db. */
    const create = async (data: MovieFormData) => {
        var model = mapFormToCreate(data);
        var resp = await createMovie(model);
        var output = mapResponseToForm(resp!);
        setModel(output);
        navigate(`./${resp?.id}`);
    };

    /** Updates exsiting entry to the db. */
    const update = async (data: MovieFormData) => {
        var model = mapFormToUpdate(data);
        var resp = await updateMovie(model);
        var output = mapResponseToForm(resp!);
        setModel(output);
    };

    return <MovieForm model={model} onSubmit={handleOnSubmit} />;
}
