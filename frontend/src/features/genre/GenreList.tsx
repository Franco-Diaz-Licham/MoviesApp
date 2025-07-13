import { Link } from "react-router-dom";
import GenreModel from "../../types/GenreModel.type";

interface GenreListProps {
    values: GenreModel[];
}

export default function GenreList(props: GenreListProps) {
    return (
        <div className="mt-3">
            <div className="mb-1">
                <div className="row">
                    <div className="col-4">
                        <div className=" input-group mb-3">
                            <span className="input-group-text" id="basic-addon1">
                                <i className="bi bi-search"></i>
                            </span>
                            <input type="text" className="form-control" placeholder="Search..." aria-label="Genre" aria-describedby="basic-addon1" />
                        </div>
                    </div>
                    <div className="col-8 d-flex justify-content-end">
                        <Link to="/genre/">
                            <button className="btn btn-success">
                                <i className="bi bi-plus-circle"></i> Add
                            </button>
                        </Link>
                    </div>
                </div>
            </div>
            <table className="table">
                <thead>
                    <tr>
                        <th scope="col"># Id</th>
                        <th scope="col">Name</th>
                        <th scope="col">Description</th>
                        <th />
                    </tr>
                </thead>
                <tbody>
                    {props.values.map((value: GenreModel) => {
                        return (
                            <tr key={value.id}>
                                <th scope="row">{value.id}</th>
                                <td>{value.name}</td>
                                <td>{value.description}</td>
                                <td className="d-flex justify-content-end">
                                    <Link to={`/genre/${value.id}`}>
                                        <button className="btn btn-primary">
                                            <i className="bi bi-pencil-fill" />
                                        </button>
                                    </Link>
                                </td>
                            </tr>
                        );
                    })}
                </tbody>
            </table>
        </div>
    );
}
