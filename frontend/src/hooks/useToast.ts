import { useContext } from "react";
import { ToastContext } from "../contexts/ToastContext";

export const useToast = () => {
    const context = useContext(ToastContext);
    if (!context) throw new Error("useAuth must be used within an ToastProvider");
    return context;
};
