import { useEffect, useState } from "react";
import { ActorResponse } from "../types/actor/ActorResponse.type";
import { deleteActor, getActors } from "../api/services/actorService";
import { Link } from "react-router-dom";
import ActorCard from "../features/actor/ActorCard";
import SearchHeader from "../components/SearchHeader";

export default function Actors() {
    const [actors, setActors] = useState<ActorResponse[]>([]);
    const [filteredActors, setFilteredActors] = useState<ActorResponse[]>(actors);

    /** Method which fetches all data. */
    const getData = async () => {
        var models = await getActors();
        if (models) setActors(models);
    };

    /** Handles deletion of a record. */
    const handleDelete = async (id: number) => {
        const result = await deleteActor(id);
        if (result === false) return;
        const models = actors.filter((model) => model.id !== id);
        setActors(models);
    };

    useEffect(() => {
        setFilteredActors(actors);
    }, [actors]);

    useEffect(() => {
        getData();
    }, []);

    return (
        <>
            <SearchHeader title="Genres" to="genre" values={actors} onSearch={(data) => setFilteredActors(data)} searchKey={(data: ActorResponse) => data.name} />
            <div>
                <div className="row g-5 justify-content-center">
                    {filteredActors.map((value) => {
                        return (
                            <div className="col-7 col-lg-3 col-md-4 d-flex col-sm-6 justify-content-center" key={value.id}>
                                <ActorCard value={value} onDelete={handleDelete} />
                            </div>
                        );
                    })}
                </div>
            </div>
        </>
    );
}
