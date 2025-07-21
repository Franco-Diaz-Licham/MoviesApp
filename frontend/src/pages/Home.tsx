import { Link } from "react-router-dom";
import { useAuth } from "../hooks/useAuth";

export default function Home() {
    const { currentUser } = useAuth();

    const getAuthContent = () => {
        return currentUser ? (
            <>
                <Link to="/movies" className="btn btn-primary btn-lg">
                    Browse Movies
                </Link>
                <Link to="/movie" className="btn btn-success btn-lg">
                    Add New Movie
                </Link>
            </>
        ) : (
            <>
                <Link to="/login" className="btn btn-primary btn-lg px-5">
                    Login
                </Link>
                <Link to="/register" className="btn btn-success btn-lg px-4">
                    Register
                </Link>
            </>
        );
    };
    return (
        <div className="container py-5">
            {/* Hero Header */}
            <header className="text-center mb-5">
                <h1 className="display-4">🎬 Welcome {currentUser?.displayName} to MoviesApp!</h1>
                <p className="lead">Your one-stop platform to browse, manage, and discover movies with ease.</p>
                <div className="d-flex justify-content-center gap-3 mt-4 flex-wrap">{getAuthContent()}</div>
            </header>

            {/* Features Section */}
            <section className="row text-center mb-5">
                <div className="col-md-4 mb-4">
                    <div className="card h-100 shadow-sm">
                        <div className="card-body">
                            <h5 className="card-title">Search & Explore</h5>
                            <p className="card-text">Quickly find movies by title, genre, or status. Filter by those in theatres or coming soon.</p>
                        </div>
                    </div>
                </div>
                <div className="col-md-4 mb-4">
                    <div className="card h-100 shadow-sm">
                        <div className="card-body">
                            <h5 className="card-title">Manage Your Collection</h5>
                            <p className="card-text">Add, edit, or delete movies and keep your catalog up to date with poster uploads and metadata.</p>
                        </div>
                    </div>
                </div>
                <div className="col-md-4 mb-4">
                    <div className="card h-100 shadow-sm">
                        <div className="card-body">
                            <h5 className="card-title">Custom Categorisation</h5>
                            <p className="card-text">Assign genres, actors, and theatres to movies for flexible sorting and filtering.</p>
                        </div>
                    </div>
                </div>
            </section>

            <section className="mb-5">
                <h2 className="text-center mb-4">🆕 Latest Features</h2>
                <ul className="list-group list-group-flush shadow-sm">
                    <li className="list-group-item">🎯 Poster image upload with real-time preview</li>
                    <li className="list-group-item">🎞️ Improved genre and actor multi-select functionality</li>
                    <li className="list-group-item">📆 Theatre-based filtering with calendar support</li>
                    <li className="list-group-item">🔒 Secure backend integration with .NET and SQL Server</li>
                </ul>
            </section>

            <section className="row align-items-center mb-5">
                <div className="col-md-6">
                    <h2>Why Choose MoviesApp?</h2>
                    <p>Built with developers and movie fans in mind, MoviesApp offers a seamless and modern user experience, easy data management, and full control over your movie database.</p>
                    <ul className="list-unstyled">
                        <li>✅ Built with React, TypeScript, and .NET</li>
                        <li>✅ Fast and mobile-friendly UI</li>
                        <li>✅ Customisable metadata support</li>
                        <li>✅ Easily extendable and open to contribution</li>
                    </ul>
                </div>
                <div className="col-md-6 text-center">
                    <img src="https://via.placeholder.com/500x300" alt="App Preview" className="img-fluid rounded shadow" />
                </div>
            </section>

            <blockquote className="blockquote text-center bg-light p-4 rounded">
                <p className="mb-0 fst-italic">“MoviesApp makes managing our internal movie collection effortless — the UI is slick and the features are exactly what we need.”</p>
                <footer className="blockquote-footer mt-2">A Happy User</footer>
            </blockquote>
        </div>
    );
}
