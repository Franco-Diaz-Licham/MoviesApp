import "./styles/App.css";
import { BrowserRouter } from "react-router-dom";
import AppRoutes from "./routes/AppRoutes";
import { useLoading } from "./hooks/useLoading";
import { useEffect } from "react";
import LoadingDisplay from "./components/LoadingDisplay";
import { LoadingProvider } from "./contexts/LoadingContext";
import api from "./api/axios";
import { setupInterceptors } from "./api/interceptor";
import { ToastProvider } from "./contexts/ToastContext";
import { useToast } from "./hooks/useToast";

function AppContent() {
    const { setLoading } = useLoading();
    const {show} = useToast();

    useEffect(() => {
        setupInterceptors(api, {show, setLoading});
    }, [setLoading]);

    return (
        <>
            <LoadingDisplay />
            <BrowserRouter>
                <AppRoutes />
            </BrowserRouter>
        </>
    );
}

function App() {
    return (
        <ToastProvider>
            <LoadingProvider>
                <AppContent />
            </LoadingProvider>
        </ToastProvider>
    );
}

export default App;
