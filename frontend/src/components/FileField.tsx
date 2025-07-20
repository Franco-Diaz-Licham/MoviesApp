import { useEffect, useState } from "react";
import { FieldErrors, UseFormRegister } from "react-hook-form";
import genericUser from "../assets/genericUser.jpg";

/** Function props. */
interface FileFieldProps {
    id: string;
    label: string;
    imageUrl?: string;
    className?: string;
    height: number;
    width: number;
    errors: FieldErrors<any>;
    register: UseFormRegister<any>;
}

/** Field component for images. */
export default function FileField(props: FileFieldProps) {
    const [previewUrl, setPreviewUrl] = useState("");

    /** Handles file content change. */
    const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const file = e.target.files?.[0];
        if (!file) return;
        const url = URL.createObjectURL(file);
        setPreviewUrl((prev) => {
            if (prev?.startsWith("blob:")) URL.revokeObjectURL(prev);
            return url;
        });
    };

    // Load preview from url
    useEffect(() => {
        if (props.imageUrl) {
            setPreviewUrl(props.imageUrl);
        } else {
            setPreviewUrl(genericUser);
        }
    }, [props.imageUrl]);

    // Perform clean up on previewURL
    useEffect(() => {
        return () => {
            if (previewUrl?.startsWith("blob:")) {
                URL.revokeObjectURL(previewUrl);
            }
        };
    }, [previewUrl]);

    return (
        <div className={props.className}>
            <div className="d-flex flex-column align-items-center">
                {previewUrl && (
                    <div className="my-3 rounded-4 p-4 border border-dark-subtle ">
                        <img src={previewUrl} alt="Preview" width={props.width} height={props.height} />
                    </div>
                )}
            </div>
            <div>
                <label htmlFor={props.id} className="form-label">
                    {props.label}
                </label>
                <input className="form-control border-dark-subtle" type="file" id={props.id} {...props.register(props.id)} accept=".png, .jpg, .jpeg" onChange={handleFileChange} />
                {props.errors[props.id] && <div className="text-danger small">{props.errors[props.id]?.message?.toString()}</div>}
            </div>
        </div>
    );
}
