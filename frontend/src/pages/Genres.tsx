import { useEffect, useState } from "react";
import { deleteGenre, getGenres } from "../api/services/genreService";
import GenreModel from "../types/genre/GenreModel.type";
import GenreTable from "../features/genre/GenreTable";
import { ApiResponse } from "../types/ApiResponse.type";
import GenreCard from "../features/genre/GenreCard";
import { Link } from "react-router-dom";

/** Genre page component. */
export default function Genres() {
    const [genres, setGenres] = useState<GenreModel[]>([]);
    const [filteredGenres, setFilteredGenres] = useState<GenreModel[]>(genres);

    /** Handle search changed. */
    const handleSearchStringChanged = (e: React.ChangeEvent<HTMLInputElement>) => {
        const searchString = e.target.value;
        const filtered = genres.filter((model) => model.name.toLowerCase().includes(searchString.toLowerCase()));
        setFilteredGenres(filtered);
    };

    /** Method which fetches all data. */
    const getData = () => {
        getGenres()
            .then((resp: ApiResponse<GenreModel>) => setGenres(resp.data))
            .catch((err) => console.error("Failed to fetch genres:", err));
    };

    /** Handles deletion of a record. */
    const handleDelete = async (id: number) => {
        const result = await deleteGenre(id);
        if (result === false) return;
        const models = genres.filter((model) => model.id !== id);
        setGenres(models);
    };

    useEffect(() => {
        setFilteredGenres(genres);
    }, [genres]);

    useEffect(() => {
        getData();
    }, []);

    return (
        <>
            <div className="mb-3 shadow-sm bg-white rounded-4 px-4 py-1">
                <h2>Genres</h2>
                <div className="row">
                    <div className="col-4">
                        <div className=" input-group mb-3">
                            <span className="input-group-text" id="basic-addon1">
                                <i className="bi bi-search"></i>
                            </span>
                            <input type="text" onChange={(e) => handleSearchStringChanged(e)} className="form-control" placeholder="Search..." aria-label="Genre" aria-describedby="basic-addon1" />
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
            <div id="genreTable" className="mb-3">
                <GenreTable values={filteredGenres} onDelete={handleDelete} />
            </div>
            <div id="genreCards">
                <div className="row g-2">
                    {filteredGenres.map((value) => {
                        return (
                            <div className="col-lg-3 col-md-4 col-sm-6">
                                <GenreCard key={value.id} value={value} onDelete={handleDelete} />
                            </div>
                        );
                    })}
                </div>
            </div>
        </>
    );
}
