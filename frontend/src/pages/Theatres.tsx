import { useEffect, useState } from "react";
import { TheatreResponse } from "../types/theatre/TheatreResponse.type";
import { deleteTheatre, getTheatres } from "../api/services/theatreService";
import SearchHeader from "../components/SearchHeader";
import TheatreAccordion from "../features/theatre/TheatreAccordion";

/** Page to display all theatres. */
export default function Theatres() {
    const [theatres, setTheatres] = useState<TheatreResponse[]>([]);
    const [filteredTheatres, setFilteredTheatres] = useState<TheatreResponse[]>(theatres);

    /** Method which gets data from server. */
    const getData = async () => {
        let models = await getTheatres();
        if (models) setTheatres(models);
    };

    /** Method which handles deletion of a theatre. */
    const handleOnDelete = async (id: number) => {
        const result = await deleteTheatre(id);
        if (result === false) return;
        const models = theatres.filter((model) => model.id !== id);
        setTheatres(models);
    };

    useEffect(() => {
        getData();
    }, []);

    useEffect(() => {
        setFilteredTheatres(theatres);
    }, [theatres]);

    return (
        <>
            <SearchHeader title="Theatres" to="theatre" values={theatres} onSearch={(data) => setFilteredTheatres(data)} searchKey={(data: TheatreResponse) => data.name} />
            <TheatreAccordion values={filteredTheatres} onDelete={handleOnDelete} />
        </>
    );
}
