import { Link } from "react-router-dom";

/** Component to be shown when a route matche no pages. */
export default function NotFoundDisplay() {
    return (
        <>
            <h1>Page Not Found {":("}</h1>
            <p>Sorry the page you are looking for does not exits.</p>
            <Link to="/">
                <button className="btn btn-info">
                    <i className="bi bi-house-door me-2"></i>
                    Go Home
                </button>
            </Link>
        </>
    );
}
