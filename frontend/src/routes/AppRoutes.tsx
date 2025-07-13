import { BrowserRouter, Route, Routes } from "react-router-dom";
import RoutesConfig from "./RoutesConfig";
import MainLayout from "../layouts/MainLayout";

export default function AppRoutes() {
    return (
        <Routes>
            {RoutesConfig.map((route) => {
                return (
                    <Route
                        path={route.path}
                        key={route.path}
                        element={
                            <MainLayout>
                                <route.component />
                            </MainLayout>
                        }
                    />
                );
            })}
        </Routes>
    );
}
