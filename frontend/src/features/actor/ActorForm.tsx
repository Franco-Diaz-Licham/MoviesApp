import TextField from "../../components/TextField";
import { useForm } from "react-hook-form";
import { Link } from "react-router-dom";
import { useEffect, useState } from "react";
import { ActorResponse } from "../../types/actor/ActorResponse.type";
import DateField from "../../components/DateField";
import MarkdownField from "../../components/MarkdownField";
import FileField from "../../components/FileField";
import { ActorUpdate } from "../../types/actor/ActorUpdate.type";
import { ActorCreate } from "../../types/actor/ActorCreate.type";
import { ActorFormData } from "../../types/actor/ActorFormData.type";

/** Function props. */
interface ActorProps {
    model: ActorFormData | null;
    onSubmit: (Data: ActorFormData) => void;
}

export default function ActorForm(props: ActorProps) {
    const initialValue = {
        defaultValues: props.model ?? { id: 0, name: "" },
    };

    const {
        register,
        handleSubmit,
        reset,
        watch,
        formState: { errors, isSubmitting },
    } = useForm<ActorFormData>(initialValue);

    useEffect(() => {
        if (props.model) reset(props.model);
    }, [props.model]);

    return (
        <form id="actorForm" className="" onSubmit={handleSubmit(props.onSubmit)} noValidate>
            <div className="shadow-sm bg-white p-4 rounded-4 mb-3">
                <h2>Actor Profile</h2>
                <div className="row mb-4">
                    <div className="col-lg-6">
                        <FileField height={150} width={150} id="image" label="Profile Image" className="mb-3" imageUrl={props.model?.imgeUrl} errors={errors} register={register} />
                    </div>
                    <div className="col-lg-6">
                        <TextField id="name" label="Name" className="mb-3" placeholder="Enter a name..." required={true} register={register} errors={errors} />
                        <DateField id="dob" label="Date of Birth" className="mb-3" required={true} register={register} errors={errors} />
                    </div>
                </div>
            </div>
            <div className="shadow-sm bg-white p-4 rounded-4">
                <MarkdownField id="biography" label="Biography" errors={errors} watch={watch} register={register} />
                <div className="d-flex gap-3 justify-content-end mt-5">
                    <Link to={"/actors"}>
                        <button type="reset" className="btn btn-danger px-4 text-uppercase fw-bold">
                            Cancel
                        </button>
                    </Link>
                    <button type="submit" className="btn btn-primary px-4 text-uppercase fw-bold" disabled={isSubmitting}>
                        Submit
                    </button>
                </div>
            </div>
        </form>
    );
}
