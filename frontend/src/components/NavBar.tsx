import { Link, NavLink } from "react-router-dom";
import logo from "../assets/logo.png";
import { useState } from "react";
import { useAuth } from "../hooks/useAuth";

/** Navbar component */
export default function NavBar() {
    const [isOpen, setIsOpen] = useState(true);
    const { currentUser, logout } = useAuth();

    /** Gets navigation link based on authentication. */
    const getAuth = () => {
        if (currentUser)
            return (
                <NavLink className="nav-link" to="" onClick={logout}>
                    Logout
                </NavLink>
            );
        else
            return (
                <NavLink className="nav-link" to="/login">
                    Login
                </NavLink>
            );
    };

    return (
        <nav className="navbar navbar-expand-lg bg-dark-subtle justify-content-between">
            <div className="container-fluid px-5 py-1">
                <Link to={"/"}>
                    <img src={logo} alt="logo" width="80" />
                </Link>
                <button className="navbar-toggler" type="button" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation" onClick={() => setIsOpen(!isOpen)}>
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className={`navbar-collapse ${isOpen ? "collapse" : "collapsed"}`} id="navbarNav" data-testid="navbar-collapse">
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
                    <ul className="navbar-nav fw-bold text-uppercase mb-lg-0 ms-3">
                        <li className="nav-item">{getAuth()}</li>
                    </ul>
                </div>
            </div>
        </nav>
    );
}
