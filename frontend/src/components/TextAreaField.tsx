import { FieldErrors, UseFormRegister } from "react-hook-form";

/** Function props. */
interface TextAreaProps {
    id: string;
    label?: string;
    rows?: number;
    required?: boolean;
    className?: string;
    placeholder?: string;
    errors: FieldErrors<any>;
    register: UseFormRegister<any>;
}

/** Textinput field. */
export default function TextAreaField(props: TextAreaProps) {
    const validation = {
        required: { value: true, message: `${props.label} is required` },
        minLength: { value: 3, message: `${props.label} must be at least 3 characters` },
    };

    return (
        <div className={props.className}>
            <label htmlFor={props.id} className="form-label">
                {props.label} {props.required && "*"}
            </label>
            <textarea id={props.id} rows={props.rows} className={`form-control ${props.errors[props.id] ? "is-invalid" : "border-dark-subtle"}`} placeholder={props.placeholder} {...props.register(props.id, validation)} />
            {props.errors[props.id] && <div className="text-danger small">{props.errors[props.id]?.message?.toString()}</div>}
        </div>
    );
}
