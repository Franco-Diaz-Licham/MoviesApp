// routes/PrivateRoute.tsx
import { Navigate } from "react-router-dom";
import { JSX } from "react";
import { useAuth } from "../hooks/useAuth";

/** Function props. */
interface PrivateRouteProps {
    children: JSX.Element;
}

/** Component which handles private routing. Redirects to login page. */
export default function PrivateRoute(props: PrivateRouteProps) {
    const { currentUser } = useAuth();
    return currentUser ? props.children : <Navigate to="/login" replace />;
}
