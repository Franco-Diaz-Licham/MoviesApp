import { NavLink } from "react-router-dom";
import logo from "../assets/logo.png"

/** Navbar component */
export default function NavBar() {
    return (
        <nav className="navbar navbar-expand-lg bg-dark-subtle justify-content-between">
            <div className="container-fluid px-5 py-1">
                <img src={logo} alt="logo" width="80" ></img>
                <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="collapse navbar-collapse" id="navbarNav">
                    <ul className="navbar-nav fw-bold text-uppercase me-auto mb-2 mb-lg-0 ms-3">
                        <li className="nav-item">
                            <NavLink className="nav-link" to="/movies">
                                Movies
                            </NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className="nav-link" to="/actors">
                                Actors
                            </NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className="nav-link" to="/theatres">
                                Theatres
                            </NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className="nav-link" to="/genres">
                                Genres
                            </NavLink>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    );
}
