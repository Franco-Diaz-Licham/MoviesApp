import { Link } from "react-router-dom";
import TextField from "../../components/TextField";
import { TheatreFormData } from "../../types/theatre/TheatreFormData.type";
import { useForm } from "react-hook-form";
import { useEffect, useState } from "react";
import MapField from "../../components/MapField";
import CoordinateDTO from "../../types/theatre/CoordinateDTO.type";

/** Function props. */
interface TheatreFormProps {
    model: TheatreFormData | null;
    onSubmit: (Data: TheatreFormData) => void;
}

/** Form to create or update theatre information. */
export default function TheatreForm(props: TheatreFormProps) {
    const [showMap, setShowMap] = useState(false);

    const initialValue = { defaultValues: props.model ?? { id: 0, name: "", latitude: -33.865143, longitude: 151.2099 } };
    const {
        register,
        handleSubmit,
        reset,
        setValue,
        watch,
        formState: { errors, isSubmitting },
    } = useForm<TheatreFormData>(initialValue);

    /** Method which handles changesin coordinates and updates form model. */
    const handleCoortinatesChanged = (data: CoordinateDTO) => {
        setValue("latitude", data.latitude);
        setValue("longitude", data.longitude);
    };

    useEffect(() => {
        if (props.model) {
            reset(props.model);
        }

        // Delay showing the map. Required to leaftlet.
        const timer = setTimeout(() => setShowMap(true), 300);
        return () => clearTimeout(timer);
    }, [props.model, reset]);

    return (
        <form id="actorForm" className="" onSubmit={handleSubmit(props.onSubmit)} noValidate>
            <div className="shadow-sm bg-white p-4 rounded-4 mb-3">
                <h2>Actor Profile</h2>
                <TextField id="name" label="Cinema Name" className="mb-3" placeholder="Enter a name..." required={true} register={register} errors={errors} />
                <TextField id="address" label="Address" className="mb-3" placeholder="Enter an address..." required={true} register={register} errors={errors} />
            </div>
            <div className="shadow-sm bg-white p-4 rounded-4">
                {showMap && <MapField disable={false} coordinates={{ latitude: watch("latitude"), longitude: watch("longitude") }} onUpdate={handleCoortinatesChanged} />}
                <div className="d-flex gap-3 justify-content-end mt-5">
                    <Link to={"/theatres"}>
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
