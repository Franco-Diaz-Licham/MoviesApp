import ReactMarkdown from "react-markdown";
import TextAreaField from "./TextAreaField";
import { FieldErrors, UseFormRegister, UseFormWatch } from "react-hook-form";
import "../styles/markdownField.css";

/** Function props */
interface MarkdownFieldProps {
    id: string;
    displayName: string;
    value: string;
    errors: FieldErrors<any>;
    watch: UseFormWatch<any>;
    register: UseFormRegister<any>;
}

/** Compoenent that interprets markdown text. */
export default function MarkdownField(props: MarkdownFieldProps) {
    return (
        <div className="mb-3 form-markdown">
            <div>
                <label htmlFor={props.id}>{props.displayName}</label>
                <TextAreaField id={props.id} label="Biography" className="form-text" rows={10} register={props.register} errors={props.errors} />
            </div>
            <div>
                <label htmlFor="preview">{props.displayName} (preview): </label>
                <div className="markdown-container">
                    <ReactMarkdown>{props.watch(props.id)}</ReactMarkdown>
                </div>
            </div>
        </div>
    );
}
