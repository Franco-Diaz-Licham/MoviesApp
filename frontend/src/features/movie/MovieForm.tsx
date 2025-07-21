import { Controller, useForm } from "react-hook-form";
import { MovieFormData } from "../../types/movie/MovieFormData.type";
import FileField from "../../components/FileField";
import TextField from "../../components/TextField";
import { Link } from "react-router-dom";
import { useEffect, useState } from "react";
import TextAreaField from "../../components/TextAreaField";
import { GenreResponse } from "../../types/genre/GenreResponse.type";
import MultiSelectField from "../../components/MultiSelectorField";
import { getGenres } from "../../api/services/genreService";
import { getActors } from "../../api/services/actorService";
import { getTheatres } from "../../api/services/theatreService";
import { ActorResponse } from "../../types/actor/ActorResponse.type";
import { TheatreResponse } from "../../types/theatre/TheatreResponse.type";
import { SwitchField } from "../../components/SwitchField";

/** Function props. */
interface MovieProps {
    model: MovieFormData | null;
    onSubmit: (Data: MovieFormData) => void;
}

/** Form to creater or update movies. */
export default function MovieForm(props: MovieProps) {
    const [genres, setGenres] = useState<GenreResponse[]>([]);
    const [actors, setActors] = useState<ActorResponse[]>([]);
    const [theatres, setTheatres] = useState<TheatreResponse[]>([]);
    const {
        register,
        handleSubmit,
        reset,
        setValue,
        watch,
        trigger,
        control,
        formState: { errors, isSubmitting },
    } = useForm<MovieFormData>({
        mode: "onChange",
        defaultValues: props.model ?? { id: 0, title: "", plot: "", genres: [], theatres: [], actors: [] },
    });

    useEffect(() => {
        if (props.model) reset(props.model);
    }, [props.model]);

    useEffect(() => {
        getData();
    }, []);

    /** Gets all dropdown data. */
    const getData = async () => {
        const [genres, actors, theatres] = await Promise.all([getGenres(), getActors(), getTheatres()]);
        if (genres) setGenres(genres);
        if (actors) setActors(actors);
        if (theatres) setTheatres(theatres);
    };

    return (
        <form id="MovieForm" className="" onSubmit={handleSubmit(props.onSubmit)} noValidate>
            <div className="shadow-sm bg-white p-4 rounded-4 mb-3">
                <h2>Movie Profile</h2>
                <div className="row mb-4">
                    <div className="col-lg-6">
                        <FileField height={250} width={150} id="image" label="Profile Image" className="mb-3" imageUrl={props.model?.imgeUrl} errors={errors} register={register} />
                    </div>
                    <div className="col-lg-6">
                        <TextField id="title" label="Name" className="mb-3" placeholder="Enter a name..." required={true} register={register} errors={errors} />
                        <TextAreaField rows={4} id="plot" label="Plot" className="mb-3" placeholder="Enter a plot..." required={true} register={register} errors={errors} />
                        <SwitchField id="inTheatresFlag" className="mb-3" label="In theatres..." required={false} register={register} errors={errors} />
                        <SwitchField id="upComingFlag" className="mb-3" label="Up-coming..." required={false} register={register} errors={errors} />
                    </div>
                </div>
            </div>
            <div className="shadow-sm bg-white p-4 rounded-4">
                <div className="row">
                    <div className="col-lg-6">
                        <Controller
                            name="genres"
                            control={control}
                            rules={{
                                validate: (value) => (value && value.length > 0) || "At least one genre must be selected",
                            }}
                            render={({ field }) => (
                                <MultiSelectField
                                    {...field}
                                    values={genres}
                                    showLabel={true}
                                    value={(model) => model.name}
                                    valueId={(model) => model.id}
                                    id="genres"
                                    label="Genres"
                                    className="mb-3"
                                    placeholder="Select genres"
                                    badgeColour="danger"
                                    required={true}
                                    errors={errors}
                                    watch={watch}
                                    setValue={setValue}
                                    trigger={trigger}
                                />
                            )}
                        />
                    </div>
                    <div className="col-lg-6">
                        <Controller
                            name="actors"
                            control={control}
                            rules={{
                                validate: (value) => (value && value.length > 0) || "At least one actor must be selected",
                            }}
                            render={({ field }) => (
                                <MultiSelectField
                                    {...field}
                                    values={actors}
                                    showLabel={true}
                                    value={(model) => model.name}
                                    valueId={(model) => model.id}
                                    id="actors"
                                    label="Actors"
                                    className="mb-3"
                                    placeholder="Select actors"
                                    badgeColour="success"
                                    required={true}
                                    errors={errors}
                                    watch={watch}
                                    setValue={setValue}
                                    trigger={trigger}
                                />
                            )}
                        />
                    </div>
                    <div className="col-lg-6">
                        <Controller
                            name="theatres"
                            control={control}
                            rules={{
                                validate: (value) => (value && value.length > 0) || "At least one theatre must be selected",
                            }}
                            render={({ field }) => (
                                <MultiSelectField
                                    {...field}
                                    values={theatres}
                                    showLabel={true}
                                    value={(model) => model.name}
                                    valueId={(model) => model.id}
                                    id="theatres"
                                    label="Theatres"
                                    className="mb-3"
                                    placeholder="Select theatres"
                                    required={true}
                                    errors={errors}
                                    watch={watch}
                                    setValue={setValue}
                                    trigger={trigger}
                                />
                            )}
                        />
                    </div>
                </div>
                <div className="d-flex gap-3 justify-content-end mt-5">
                    <Link to={"/Movies"}>
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
