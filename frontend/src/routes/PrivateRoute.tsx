// routes/PrivateRoute.tsx
import { Navigate } from "react-router-dom";
import { JSX } from "react";
import { useAuth } from "../hooks/useAuth";

/** Function props. */
interface PrivateRouteProps {
  children: JSX.Element;
}

const PrivateRoute = (props: PrivateRouteProps) => {
  const { currentUser } = useAuth();
  return currentUser ? props.children : <Navigate to="/login" replace />;
};

export default PrivateRoute;

