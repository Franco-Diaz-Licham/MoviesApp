import { Link } from "react-router-dom";
import { MovieResponse } from "../../types/movie/MovieResponse.type";
import BadgeField from "../../components/Badgefield";

interface MovieCardProps {
    value: MovieResponse;
    onDelete: (id: number) => void;
}

export default function MovieCard(props: MovieCardProps) {
    return (
        <div className="card shadow-sm bg-white border-0 rounded-4">
            <img src={props.value.photo.publicUrl} className="rounded-top-4" alt="..." />
            <div className="card-body">
                <h5 className="card-title">{props.value.title}</h5>
                <div className="d-flex justify-content-between">
                    <span className="card-subtitle mb-2 text-body-secondary small">{props.value.inTheatresFlag ? "In Theatres" : "Upcoming"}</span>
                </div>
                <div className="card-text overflow-hidden" style={{ height: "3rem" }}>
                    <span className="me-1">Starring:</span>
                    {props.value.actors?.map((data) => (
                        <Link to={`/actor/${data.id}`} key={data.id}><BadgeField name={data.name} colour="success" /></Link>
                    ))}
                </div>
                <div className="card-text overflow-hidden" style={{ height: "3rem" }}>
                    <span className="me-1">Genres:</span>
                    {props.value.genres?.map((data) => (
                        <BadgeField name={data.name} key={data.id} colour="danger"/>
                    ))}
                </div>
                <div className="d-flex justify-content-end mt-3">
                    <Link to={`/Movie/${props.value.id}`} >
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
