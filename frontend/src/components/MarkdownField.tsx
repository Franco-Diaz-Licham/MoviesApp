import ReactMarkdown from "react-markdown";
import TextAreaField from "./TextAreaField";
import { FieldErrors, UseFormRegister, UseFormWatch } from "react-hook-form";
import "../styles/MarkdownField.css";

/** Function props */
interface MarkdownFieldProps {
    id: string;
    label: string;
    errors: FieldErrors<any>;
    watch: UseFormWatch<any>;
    register: UseFormRegister<any>;
}

/** Compoenent that interprets markdown text. */
export default function MarkdownField(props: MarkdownFieldProps) {
    return (
        <div className="row">
            <TextAreaField className="col-lg-6 col-md-6 col-sm-12" id={props.id} label={props.label} rows={12} register={props.register} errors={props.errors} />
            <div className="col-lg-6 col-md-6 col-sm-12">
                <label htmlFor="preview" className="form-label">
                    {props.label} (preview):
                </label>
                <div className="markdown-container p-2 rounded-2">
                    <ReactMarkdown>{props.watch(props.id)}</ReactMarkdown>
                </div>
            </div>
        </div>
    );
}
