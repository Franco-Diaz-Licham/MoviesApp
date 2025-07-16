import { Link } from "react-router-dom";
import GenreModel from "../../types/genre/GenreModel.type";

/** Function props. */
interface GenreCardProps {
    value: GenreModel;
    onDelete: (id: number) => void;
}

/** Gender card component used when view port is of mobile phone size. */
export default function GenreCard(props: GenreCardProps) {
    return (
        <div className="card shadow-sm bg-white border-0 rounded-4 px-2 py-1" style={{ height: "11rem" }}>
            <div className="card-body p-2">
                <h5 className="card-title">{props.value.name}</h5>
                <h6 className="card-subtitle mb-2 text-body-secondary">#{props.value.id}</h6>
                <p className="card-text overflow-hidden" style={{ height: "3rem" }}>
                    {props.value.description}
                </p>
                <div className="d-flex justify-content-end">
                    <Link to={`/genre/${props.value.id}`}>
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
