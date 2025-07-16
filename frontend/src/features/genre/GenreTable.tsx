import { Link } from "react-router-dom";
import GenreModel from "../../types/genre/GenreModel.type";
import css from "../../styles/GenreTable.module.css";

/** Function props. */
interface GenreListProps {
    values: GenreModel[];
    onDelete: (id: number) => void;
}

/** Table to see genre records used for when the view port is greater than that of a mobile phone. */
export default function GenreTable(props: GenreListProps) {
    return (
        <div className="shadow-sm bg-white rounded-4 p-4">
            <div className={`${css.tableHeight}`}>
                <table className="table">
                    <thead>
                        <tr>
                            <th scope="col" className="ps-4">
                                # Id
                            </th>
                            <th scope="col">Name</th>
                            <th scope="col" className="pe-4">
                                Description
                            </th>
                            <th />
                        </tr>
                    </thead>
                    <tbody>
                        {props.values.map((value: GenreModel) => {
                            return (
                                <tr key={value.id}>
                                    <th scope="row" className="text-decoration-underline text-primary ps-4">
                                        # {value.id}
                                    </th>
                                    <td>{value.name}</td>
                                    <td>{value.description}</td>
                                    <td className="d-flex justify-content-end pe-4">
                                        <button className="btn btn-danger me-2" onClick={() => props.onDelete(value.id)}>
                                            <i className="bi bi-trash-fill" />
                                        </button>
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
        </div>
    );
}
