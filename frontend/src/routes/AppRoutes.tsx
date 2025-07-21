import { Routes, Route } from "react-router-dom";
import RoutesConfig, { RouteModel } from "./RoutesConfig";
import MainLayout from "../layouts/MainLayout";
import PrivateRoute from "./PrivateRoute";

/** Router main component. */
export default function AppRoutes() {
  const getRouting = (route: RouteModel) => {
    const element = (
      <MainLayout>
        <route.component />
      </MainLayout>
    );

    return (
      <Route
        path={route.path}
        key={route.path}
        element={
          route.auth ? <PrivateRoute>{element}</PrivateRoute> : element
        }
      />
    );
  };

  return <Routes>{RoutesConfig.map((route) => getRouting(route))}</Routes>;
}
