import { UseFormRegister } from "react-hook-form";

export default function DateField(props: DateFieldProps) {
    /** Gets required symbol for the UI. */
    const getRequired = () => (!props.required ? props.label : `${props.label} *`);

    return (
        <div className="mb-3">
            <label htmlFor={props.field} className="form-label">
                {getRequired()}
            </label>
            <input id={props.field} className={props.className} type="date" {...props.register(props.id, { required: props.required ?? false })} />
        </div>
    );
}

/** DateField props. */
interface DateFieldProps {
    id: string;
    field: string;
    label: string;
    className?: string;
    required?: boolean;
    register: UseFormRegister<any>;
}
