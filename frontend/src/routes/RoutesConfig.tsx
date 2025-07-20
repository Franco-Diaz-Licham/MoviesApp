import path from "path";
import Genres from "../pages/Genres";
import Home from "../pages/Home";
import Actors from "../pages/Actors";
import Theatres from "../pages/Theatres";
import Movies from "../pages/Movies";
import GenreProfile from "../pages/GenreProfile";
import NotFound from "../components/NotFoundDisplay";
import ActorProfile from "../pages/ActorProfile";
import TheatreProfile from "../pages/TheatreProfile";
import MovieProfile from "../pages/MovieProfile";

const RoutesConfig = [
    { path: "/genres", exact: true, component: Genres },
    { path: "/genre/:id", exact: true, component: GenreProfile },
    { path: "/genre/", exact: true, component: GenreProfile },
    { path: "/movies", component: Movies },
    { path: "/movie/:id", component: MovieProfile },
    { path: "/movie/", component: MovieProfile },
    { path: "/actors", component: Actors },
    { path: "/actor/:id", component: ActorProfile },
    { path: "/actor/", component: ActorProfile },
    { path: "/theatres", component: Theatres },
    { path: "/theatre/", component: TheatreProfile },
    { path: "/theatre/:id", component: TheatreProfile },
    { path: "/", component: Home },
    { path: "*", component: NotFound },
];

export default RoutesConfig;
