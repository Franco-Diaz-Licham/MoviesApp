import { Link } from "react-router-dom";

interface SearchHeaderProps<T> {
    values: T[];
    to: string;
    title: string;
    onSearch: (data: T[]) => void;
    searchKey: (model: T) => string;
}

export default function SearchHeader<T>(props: SearchHeaderProps<T>) {
    const handleSearchStringChanged = (e: React.ChangeEvent<HTMLInputElement>) => {
        const searchString = e.target.value.toLowerCase();
        const filtered = props.values.filter((model) => props.searchKey(model).toLowerCase().includes(searchString));
        props.onSearch(filtered);
    };

    return (
        <div className="mb-3 shadow-sm bg-white rounded-4 px-4 py-1">
            <h2>{props.title}</h2>
            <div className="row">
                <div className="col-lg-6 col-md-6 col-sm-8 col-8">
                    <div className="input-group mb-3">
                        <span className="input-group-text" id="basic-addon1">
                            <i className="bi bi-search"></i>
                        </span>
                        <input type="text" onChange={handleSearchStringChanged} className="form-control" placeholder="Search..." aria-label={props.title} aria-describedby="basic-addon1" />
                    </div>
                </div>
                <div className="col-lg-6 col-md-6 col-sm-4 col-4 d-flex justify-content-end">
                    <Link to={`/${props.to}/`}>
                        <button className="btn btn-success">
                            <i className="bi bi-plus-circle"></i> Add
                        </button>
                    </Link>
                </div>
            </div>
        </div>
    );
}
