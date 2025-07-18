import { useEffect, useState } from "react";
import TheatreForm from "../features/theatre/TheatreForm";
import { TheatreFormData } from "../types/theatre/TheatreFormData.type";
import { Params, useNavigate, useParams } from "react-router-dom";
import { createTheatre, getTheatresById, updateTheatre } from "../api/services/theatreService";
import { mapFormToCreate, mapFormToUpdate, mapResponseToForm } from "../utils/TheatreMapper";

export default function TheatreProfile() {
    const [model, setModel] = useState<TheatreFormData | null>(null);
    const { id }: Params<string> = useParams();
    const navigate = useNavigate();

    useEffect(() => {
        if (!id) return;
        fetchData(id);
    }, []);

    /** Gets data from the server. */
    const fetchData = async (id: string) => {
        const modelId = Number.parseInt(id);
        const resp = await getTheatresById(modelId);
        const model = mapResponseToForm(resp!);
        setModel(model);
    };

    /** Handles the saving of data entry. */
    const handleOnSubmit = async (data: TheatreFormData) => {
        if (id) return await update(data);
        await create(data);
    };

    /** Saves new entry to the Db. */
    const create = async (data: TheatreFormData) => {
        var model = mapFormToCreate(data);
        var resp = await createTheatre(model);
        var output = mapResponseToForm(resp!);
        setModel(output);
        navigate(`./${resp?.id}`);
    };

    /** Updates exsiting entry to the db. */
    const update = async (data: TheatreFormData) => {
        var model = mapFormToUpdate(data);
        var resp = await updateTheatre(model);
        var output = mapResponseToForm(resp!);
        setModel(output);
    };

    return <TheatreForm model={model} onSubmit={handleOnSubmit} />;
}
