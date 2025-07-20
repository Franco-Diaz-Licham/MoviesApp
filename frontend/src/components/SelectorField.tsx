import { FieldError, FieldErrors, UseFormRegister } from "react-hook-form";
import { BaseSelectorModel } from "../types/BaseSelectorModel.type";

/** Function props. Type T will have id and name properties as base. */
interface SelectorProps<T extends BaseSelectorModel> {
    id: string;
    name: string;
    label: string;
    required?: boolean;
    showLabel?: boolean;
    className?: string;
    values: T[];
    placeholder?: string;
    error?: FieldError;
    selectedValueId: number;
    errors: FieldErrors<any>;
    register: UseFormRegister<any>;
}

/** Generic selector field for all dropdowns. */
export default function SelectorField<T extends BaseSelectorModel>(props: SelectorProps<T>) {
    return (
        <div className={props.className}>
            {props.showLabel && (
                <label htmlFor={props.name} className="form-label mb-0">
                    {props.label} {props.required && "*"}
                </label>
            )}
            <select id={props.id} {...props.register(props.id, { required: props.required ?? false })} className="form-select">
                <option value={props.selectedValueId ?? ""} disabled selected>
                    {props.placeholder}
                </option>
                {props.values.map((item) => (
                    <option key={item.id} value={item.id}>
                        {item.selectionValue()}
                    </option>
                ))}
            </select>
            {props.errors[props.id] && <div className="text-danger small">{props.errors[props.id]?.message?.toString()}</div>}
        </div>
    );
}
