import { useEffect, useState } from "react";
import { deleteGenre, getGenres } from "../api/services/genreService";
import GenreTable from "../features/genre/GenreTable";
import GenreCard from "../features/genre/GenreCard";
import { GenreResponse } from "../types/genre/GenreResponse.type";
import SearchHeader from "../components/SearchHeader";

/** Genre page component. */
export default function Genres() {
    const [genres, setGenres] = useState<GenreResponse[]>([]);
    const [filteredGenres, setFilteredGenres] = useState<GenreResponse[]>(genres);
    
    /** Method which fetches all data. */
    const getData = async () => {
        let models = await getGenres();
        if(models) setGenres(models);
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
            <SearchHeader title="Genres" to="genre" values={genres} onSearch={(data) => setFilteredGenres(data)} searchKey={(data: GenreResponse) => data.name}/>
            <div id="genreTable" className="mb-3">
                <GenreTable values={filteredGenres} onDelete={handleDelete} />
            </div>
            <div id="genreCards">
                <div className="row g-2">
                    {filteredGenres.map((value) => {
                        return (
                            <div className="col-lg-3 col-md-4 col-sm-6" key={value.id}>
                                <GenreCard value={value} onDelete={handleDelete} />
                            </div>
                        );
                    })}
                </div>
            </div>
        </>
    );
}
