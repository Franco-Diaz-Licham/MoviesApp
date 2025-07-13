import { useEffect, useState } from "react";
import { getGenres } from "../api/services/genreService";
import GenreModel from "../types/GenreModel.type";
import GenreList from "../features/genre/GenreList";

export default function Genres() {
    const [genres, setGenres] = useState<GenreModel[]>([]);

    useEffect(() => {
        getData();
    }, []);

    const getData = () => {
        getGenres()
            .then((response) => setGenres(response.data))
            .catch((err) => console.error("Failed to fetch genres:", err));
    };

    return (
        <div>
            <h1>Genres</h1>
            <GenreList values={genres} />
        </div>
    );
}
