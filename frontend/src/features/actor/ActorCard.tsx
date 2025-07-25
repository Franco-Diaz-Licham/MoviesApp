import { ActorResponse } from "../../types/actor/ActorResponse.type";
import { Link } from "react-router-dom";
import ReactMarkdown from "react-markdown";

/** Function props. */
interface ActorCardProps {
    value: ActorResponse;
    onDelete: (id: number) => void;
}

/** Actor card component. */
export default function ActorCard(props: ActorCardProps) {
    return (
        <div className="card shadow-sm bg-white border-0 rounded-4" >
            <img src={props.value.photo.publicUrl} className="rounded-top-4" alt="..."  />
            <div className="card-body">
                <h5 className="card-title">{props.value.name}</h5>
                <div className="d-flex justify-content-between">
                    <span className="card-subtitle mb-2 text-body-secondary small">#{props.value.id}</span>
                    <span className="card-subtitle mb-2 text-body-secondary small">D.O.B: {props.value.dob.toString()}</span>
                </div>
                <div className="card-text overflow-hidden" style={{ height: "3rem" }}>
                    <ReactMarkdown>{props.value.biography}</ReactMarkdown>
                </div>
                <div className="d-flex justify-content-end mt-3">
                    <Link to={`/actor/${props.value.id}`}>
                        <i className="bi bi-pencil-fill fs-5" />
                    </Link>
                    <Link to={""}>
                        <i className="ms-3 bi bi-trash-fill text-danger fs-5" onClick={() => props.onDelete(props.value.id)} />
                    </Link>
                </div>
            </div>
        </div>
    );
}
