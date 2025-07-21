
/** Function props. */
interface BadgeFieldProps {
    name: string;
    colour?: string;
    onRemove?: () => void;
}
/** Badge component. */
export default function BadgeField(props: BadgeFieldProps) {
    return (
        <span className={`badge bg-${props.colour ?? "primary" }`}>
            {props.name}
            {props.onRemove && (
                <button type="button" className="btn-close btn-close-white btn-sm ms-2" aria-label="Remove" onClick={props.onRemove}></button>
            )}
        </span>
    );
}
