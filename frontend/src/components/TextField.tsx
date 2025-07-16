import { FieldErrors, UseFormRegister } from "react-hook-form";

/** Function props */
interface TextFieldProps {
    id: string;
    label?: string;
    required?: boolean;
    className?: string;
    placeholder?: string;
    type?: string;
    errors: FieldErrors<any>;
    register: UseFormRegister<any>;
}

/** Input field. */
export default function TextField(props: TextFieldProps) {
    const validation = {
        required: { value: true, message: `${props.label} is required` },
        minLength: { value: 3, message: `${props.label} must be at least 3 characters` },
    };

    return (
        <div className={props.className}>
            <label htmlFor={props.id} className="form-label">
                {props.label} {props.required && "*"}
            </label>
            <input type={props.type ?? "text"} className={`form-control ${props.errors[props.id] ? "is-invalid" : "border-dark-subtle"}`} id={props.id} placeholder={props.placeholder} {...props.register(props.id, validation)} />
            {props.errors[props.id] && <div className="text-danger small">{props.errors[props.id]?.message?.toString()}</div>}
        </div>
    );
}
