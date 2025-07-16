import { useState, useRef, useEffect } from "react";
import { FieldErrors, UseFormRegister } from "react-hook-form";
import { BaseSelectoModel } from "../types/BaseSelectoModel.type";

interface MultiSelectFieldProps<T extends BaseSelectoModel> {
    id: string;
    values: T[];
    selected: T[];
    label: string;
    placeholder: string;
    showLabel?: boolean;
    className?: string;
    required?: boolean;
    errors: FieldErrors<any>;
    register: UseFormRegister<any>;
}

export default function MultiSelectField<T extends BaseSelectoModel>(props: MultiSelectFieldProps<T>) {
    return (
        <>
            <div className={props.className}>
                {props.showLabel && (
                    <label htmlFor={props.id} className="form-label mb-0">
                        {props.label} {props.required && "*"}
                    </label>
                )}

                <select id={props.id} {...props.register(props.id, { required: props.required ?? false })} className="form-select">
                    {/* <option value={props.selectedValueId ?? ""} disabled selected>
                        {props.placeholder}
                    </option> */}
                    {props.values.map((item) => (
                        <option key={item.id} value={item.id}>
                            {item.name}
                        </option>
                    ))}
                </select>
                {props.errors[props.id] && <div className="text-danger small">{props.errors[props.id]?.message?.toString()}</div>}
            </div>
        </>
    );
}
