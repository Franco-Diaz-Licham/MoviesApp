import { Params, useNavigate, useParams } from "react-router-dom";
import GenreForm from "../features/genre/GenreForm";
import GenreModel from "../types/GenreModel.type";
import { createGenre, updateGenre } from "../api/services/genreService";

export default function GenreProfile() {
    let model: GenreModel | null = null;
    const navigate = useNavigate();
    const { id }: Params<string> = useParams();

    const handleOnSubmit = (data: GenreModel) => {
        if (id) updateGenre(data);
        else createGenre(data);

        console.log(data);
        // navigate("./genres");
    };

    return (
        <>
            <h2>Profile</h2>
            <GenreForm model={model} onSubmit={handleOnSubmit} />
        </>
    );
}
