import { useLoading } from "../hooks/useLoading";
import css from "../styles/LoadingDisplay.module.css";
import loader from "../assets/loading.gif"

/** Loading display to be used when transition pages and loading data. */
export default function LoadingDisplay() {
    const { loading } = useLoading();
    if (!loading) return null;

    return (
        <>
            <div className={`${css.overlay} bg-dark-subtle opacity-25`}></div>
            <div className={`${css.loadingDisplay}`}>
                <img src={loader} alt="loading" />
            </div>
        </>
    );
}
