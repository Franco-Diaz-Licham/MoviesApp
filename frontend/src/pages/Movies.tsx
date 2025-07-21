import { useEffect, useState } from "react";
import { MovieResponse } from "../types/movie/MovieResponse.type";
import SearchHeader from "../components/SearchHeader";
import MovieCard from "../features/movie/MovieCard";
import { deleteMovie, getMovies } from "../api/services/movieService";

/** Page to display all movies. */
export default function Movies() {
    const [Movies, setMovies] = useState<MovieResponse[]>([]);
    const [filteredMovies, setFilteredMovies] = useState<MovieResponse[]>(Movies);

    /** Method which fetches all data. */
    const getData = async () => {
        var models = await getMovies();
        if (models) setMovies(models);
    };

    /** Handles deletion of a record. */
    const handleDelete = async (id: number) => {
        const result = await deleteMovie(id);
        if (result === false) return;
        const models = Movies.filter((model) => model.id !== id);
        setMovies(models);
    };

    useEffect(() => {
        setFilteredMovies(Movies);
    }, [Movies]);

    useEffect(() => {
        getData();
    }, []);

    return (
        <>
            <SearchHeader title="Movies" to="movie" values={Movies} onSearch={(data) => setFilteredMovies(data)} searchKey={(data: MovieResponse) => data.title} />
            <div className="row g-5 justify-content-center">
                {filteredMovies.map((value) => {
                    return (
                        <div className="col-7 col-lg-3 col-md-4 d-flex col-sm-6 justify-content-center" key={value.id}>
                            <MovieCard value={value} onDelete={handleDelete} />
                        </div>
                    );
                })}
            </div>
        </>
    );
}
