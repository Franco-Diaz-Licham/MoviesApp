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
import { JSX } from "react";
import Login from "../pages/Login";

export interface RouteModel {
    path: string;
    exact: boolean;
    component: () => JSX.Element;
    auth: boolean;
}

/** Route list configuration. */
const RoutesConfig: RouteModel[] = [
    { path: "/genres", exact: true, component: Genres, auth: true },
    { path: "/genre/:id", exact: true, component: GenreProfile, auth: true },
    { path: "/genre/", exact: true, component: GenreProfile, auth: true },
    { path: "/movies", exact: false, component: Movies, auth: true },
    { path: "/movie/:id", exact: false, component: MovieProfile, auth: true },
    { path: "/movie/", exact: false, component: MovieProfile, auth: true },
    { path: "/actors", exact: false, component: Actors, auth: true },
    { path: "/actor/:id", exact: false, component: ActorProfile, auth: true },
    { path: "/actor/", exact: false, component: ActorProfile, auth: true },
    { path: "/theatres", exact: false, component: Theatres, auth: true },
    { path: "/theatre/", exact: false, component: TheatreProfile, auth: true },
    { path: "/theatre/:id", exact: false, component: TheatreProfile, auth: true },
    { path: "/login/", exact: false, component: Login, auth: false },
    { path: "/", exact: false, component: Home, auth: false },
    { path: "*", exact: false, component: NotFound, auth: false },
];

export default RoutesConfig;
