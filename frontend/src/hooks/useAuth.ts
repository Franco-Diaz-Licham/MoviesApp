import { useContext } from "react";
import { AuthContext } from "../contexts/AuthContext";

/**
 * Export this custom hook to use the context safely
 */
export const useAuth = () => {
    const context = useContext(AuthContext);
    if (!context) throw new Error("useAuth must be used within an AuthProvider");
    return context;
};
