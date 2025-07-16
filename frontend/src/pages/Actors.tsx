import { useEffect, useState } from "react";
import { ActorResponse } from "../types/actor/ActorResponse.type";
import { deleteActor, getActors } from "../api/services/actorService";
import { Link } from "react-router-dom";
import ActorCard from "../features/actor/ActorCard";

export default function Actors() {
    const [actors, setActors] = useState<ActorResponse[]>([]);
    const [filteredActors, setFilteredActors] = useState<ActorResponse[]>(actors);

    /** Handle search changed. */
    const handleSearchStringChanged = (e: React.ChangeEvent<HTMLInputElement>) => {
        const searchString = e.target.value;
        const filtered = actors.filter((model) => model.name.toLowerCase().includes(searchString.toLowerCase()));
        setFilteredActors(filtered);
    };

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
            <div className="mb-3 shadow-sm bg-white rounded-4 px-4 py-1">
                <h2>Actors</h2>
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
                        <Link to="/actor/">
                            <button className="btn btn-success">
                                <i className="bi bi-plus-circle"></i> Add
                            </button>
                        </Link>
                    </div>
                </div>
            </div>
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
