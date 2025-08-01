import { FieldErrors, UseFormRegister } from "react-hook-form";

/** Function props. */
interface DateFieldProps {
    id: string;
    label: string;
    className?: string;
    required?: boolean;
    errors: FieldErrors<any>;
    register: UseFormRegister<any>;
}

/** DateField component. */
export default function DateField(props: DateFieldProps) {
    return (
        <div className={props.className}>
            <label htmlFor={props.id} className="form-label">
                {props.label} {props.required && "*"}
            </label>
            <input id={props.id} className={`form-control ${props.errors[props.id] ? "is-invalid" : "border-dark-subtle"}`} type="date" {...props.register(props.id, { required: props.required ?? false })} />
            {props.errors[props.id] && <div className="text-danger small">{props.errors[props.id]?.message?.toString()}</div>}
        </div>
    );
}
