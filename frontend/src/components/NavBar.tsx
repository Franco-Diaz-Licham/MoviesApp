import { NavLink } from "react-router-dom";

/** Navbar component */
export default function NavBar() {
    return (
        <nav className="navbar navbar-expand-lg navbar-light bg-light mb-3">
            <div className="container">
                <NavLink className="navbar-brand" to="/">
                    Movies
                </NavLink>
                <div className="collpase navbar-collapse">
                    <ul className="navbar-nav me-auto mb-2 mb-lg-0">
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
