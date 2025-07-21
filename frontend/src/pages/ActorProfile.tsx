import { Params, useNavigate, useParams } from "react-router-dom";
import ActorForm from "../features/actor/ActorForm";
import { useEffect, useState } from "react";
import { createActor, getActorsById, updateActor } from "../api/services/actorService";
import { ActorFormData } from "../types/actor/ActorFormData.type";
import { mapFormToCreate, mapFormToUpdate, mapResponseToForm } from "../utils/ActorMapper";

/** Main actor profile page. */
export default function ActorProfile() {
    const [model, setModel] = useState<ActorFormData | null>(null);
    const { id }: Params<string> = useParams();
    const navigate = useNavigate();

    useEffect(() => {
        if (!id) return;
        fetchData(id);
    }, []);

    /** Gets data from the server. */
    const fetchData = async (id: string) => {
        const modelId = Number.parseInt(id);
        const resp = await getActorsById(modelId);
        const model = mapResponseToForm(resp!);
        setModel(model);
    };

    /** Handles the saving of data entry. */
    const handleOnSubmit = async (data: ActorFormData) => {
        if (id) return await update(data);
        await create(data);
    };

    /** Saves new entry to the Db. */
    const create = async (data: ActorFormData) => {
        var model = mapFormToCreate(data);
        var resp = await createActor(model);
        var output = mapResponseToForm(resp!);
        setModel(output);
        navigate(`./${resp?.id}`);
    };

    /** Updates exsiting entry to the db. */
    const update = async (data: ActorFormData) => {
        var model = mapFormToUpdate(data);
        var resp = await updateActor(model);
        var output = mapResponseToForm(resp!);
        setModel(output);
    };

    return <ActorForm model={model} onSubmit={handleOnSubmit} />;
}
