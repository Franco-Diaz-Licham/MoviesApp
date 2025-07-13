import TextField from "../../components/TextField";
import { useForm } from "react-hook-form";
import GenreModel from "../../types/GenreModel.type";
import { Link } from "react-router-dom";

interface GenreProps {
    model: GenreModel | null;
    onSubmit: (Data: GenreModel) => void;
}

export default function GenreForm(props: GenreProps) {
    const {
        register,
        handleSubmit,
        formState: { errors, isSubmitting },
    } = useForm<GenreModel>();

    return (
        <form id="genreForm" className="border-1 shadow-lg p-5 rounded-4" onSubmit={handleSubmit(props.onSubmit)} noValidate>
            <TextField id="title" label="Title" className="mb-3" placeholder="Enter a title..." required={true} register={register} errors={errors} />
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
