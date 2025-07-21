import { FieldErrors, UseFormSetValue, UseFormTrigger, UseFormWatch } from "react-hook-form";
import BadgeField from "./Badgefield";

/** Function props. */
interface MultiSelectFieldProps<T> {
    id: string;
    values: T[];
    label: string;
    placeholder: string;
    showLabel?: boolean;
    className?: string;
    required?: boolean;
    badgeColour?: string;
    value: (model: T) => string;
    valueId: (model: T) => number;
    errors: FieldErrors<any>;
    watch: UseFormWatch<any>;
    setValue: UseFormSetValue<any>;
    trigger: UseFormTrigger<any>;
}

/** Multiselect component. */
export default function MultiSelectField<T>(props: MultiSelectFieldProps<T>) {
    const selectedValues: number[] = props.watch(props.id) ?? [];

    /** Handles selection and forces validation. */
    const handleSelectedChanged = (e: React.ChangeEvent<HTMLSelectElement>) => {
        const id = Number(e.currentTarget.value);
        if (!id || selectedValues.includes(id)) return;
        const updated = [...selectedValues, id];
        props.setValue(props.id, updated);
        props.trigger(props.id);
        e.currentTarget.selectedIndex = 0;
    };

    /** Handles options removal. */
    const handleRemove = (id: number) => {
        const updated = selectedValues.filter((i) => i !== id);
        props.setValue(props.id, updated);
        props.trigger(props.id);
    };

    return (
        <div className={props.className}>
            {props.showLabel && (
                <label htmlFor={`select-${props.id}`} className="form-label mb-0">
                    {props.label} {props.required && "*"}
                </label>
            )}
            <select id={`select-${props.id}`} className={`form-select ${props.errors[props.id] ? "is-invalid" : "border-dark-subtle"}`} onChange={handleSelectedChanged}>
                <option value="">-- {props.placeholder} --</option>
                {props.values.map((model) => (
                    <option value={props.valueId(model)} key={props.valueId(model)}>
                        {props.value(model)}
                    </option>
                ))}
            </select>
            <div className="mt-2 d-flex flex-wrap gap-2">
                {selectedValues.map((id) => {
                    const model = props.values.find((v) => props.valueId(v) === id);
                    return model ? <BadgeField key={id} name={props.value(model)} onRemove={() => handleRemove(id)} colour={props.badgeColour} /> : null;
                })}
            </div>
            {props.errors[props.id] && <div className="text-danger small">{props.errors[props.id]?.message?.toString()}</div>}
        </div>
    );
}
