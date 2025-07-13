import { useEffect, useState } from "react";
import { FieldErrors, UseFormRegister } from "react-hook-form";

/** Function props. */
interface FileFieldProps {
    id: string;
    label: string;
    imageUrl?: string;
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
        <>
            <label htmlFor={props.id} className="form-label">
                {props.label}
            </label>
            <input className="form-control" type="file" id={props.id} {...props.register(props.id)} accept=".png, .jpg, .jpeg" onChange={handleFileChange} />
            {props.errors[props.id] && <div className="text-danger small">{props.errors[props.id]?.message?.toString()}</div>}
            {previewUrl && (
                <div className="my-3">
                    <img src={previewUrl} alt="Preview" className="w-25" />
                </div>
            )}
        </>
    );
}
