import { FieldErrors, UseFormRegister } from "react-hook-form";

/** Function props. */
interface SwitchFieldProps {
    id: string;
    required?: boolean;
    className?: string;
    label?: string;
    type?: string;
    errors: FieldErrors<any>;
    register: UseFormRegister<any>;
}

/** Switch component. */
export function SwitchField(props: SwitchFieldProps) {
    return (
        <div className={props.className}>
            <div className="form-check form-switch">
                <input
                    className={`form-check-input fs-5 ${props.errors[props.id] ? "is-invalid" : "border-dark-subtle"}`}
                    type="checkbox"
                    value=""
                    id={props.id}
                    {...props.register(props.id, { required: { value: props.required ?? false, message: `${props.label} is required` } })}
                />
                <label className="form-check-label" htmlFor={props.id}>
                    {props.label} {props.required && "*"}
                </label>
                {props.errors[props.id] && <div className="text-danger small">{props.errors[props.id]?.message?.toString()}</div>}
            </div>
        </div>
    );
}
