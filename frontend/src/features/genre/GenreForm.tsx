import TextField from "../../components/TextField";
import { useForm } from "react-hook-form";
import GenreFromData from "../../types/genre/GenreFormData.type";
import { Link } from "react-router-dom";
import { useEffect } from "react";

/** Function props. */
interface GenreProps {
    model: GenreFromData | null;
    onSubmit: (Data: GenreFromData) => void;
}

/** Gender from used for creating or updating records. */
export default function GenreForm(props: GenreProps) {
    const initialValue = {
        defaultValues: props.model ?? { id: 0, name: "" },
    };

    const {
        register,
        handleSubmit,
        reset,
        formState: { errors, isSubmitting },
    } = useForm<GenreFromData>(initialValue);

    useEffect(() => {
        if (props.model) reset(props.model);
    }, [props.model]);

    return (
        <form id="genreForm" className="shadow-sm bg-white p-4 rounded-4" onSubmit={handleSubmit(props.onSubmit)} noValidate>
            <h2>Genre Profile</h2>
            <TextField id="name" label="Name" className="mb-3" placeholder="Enter a name..." required={true} register={register} errors={errors} />
            <TextField id="description" label="Description" className="mb-3" placeholder="Enter a short descriptive..." required={true} register={register} errors={errors} />
            <div className="d-flex gap-3 justify-content-end mt-5">
                <Link to={"/genres"}>
                    <button type="reset" className="btn btn-danger px-4 text-uppercase fw-bold">
                        Cancel
                    </button>
                </Link>
                <button type="submit" className="btn btn-primary px-4 text-uppercase fw-bold" disabled={isSubmitting}>
                    Submit
                </button>
            </div>
        </form>
    );
}
