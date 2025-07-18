import { NavLink } from "react-router-dom";
import logo from "../assets/logo.png";
import { useState } from "react";

/** Navbar component */
export default function NavBar() {
    const [isOpen, setIsOpen] = useState(true);

    return (
        <nav className="navbar navbar-expand-lg bg-dark-subtle justify-content-between">
            <div className="container-fluid px-5 py-1">
                <img src={logo} alt="logo" width="80"></img>
                <button className="navbar-toggler" type="button" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation" onClick={() => setIsOpen(!isOpen)}>
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className={`navbar-collapse ${isOpen ? "collapse" : "collapsed"}`} id="navbarNav">
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
