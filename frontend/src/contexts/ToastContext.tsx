import { createContext, useRef, useState } from "react";
import { Toast as BootstrapToast } from "bootstrap";
import Toast from "../components/Toast";

interface ToastContextType {
    show: (msg: string, options?: ToastOptions) => void;
}

interface ToastOptions {
    delay?: number;
    result?: string;
}

export const ToastProvider = ({ children }: { children: React.ReactNode }) => {
    const toastRef = useRef<HTMLDivElement>(null);
    const [message, setMessage] = useState("");
    const [resultType, setResultType] = useState("");

    const show = (msg: string, options?: ToastOptions) => {
        // update state variables
        setMessage(msg);
        setResultType(options?.result || "success");

        const toast = BootstrapToast.getOrCreateInstance(toastRef.current!, {
            delay: options?.delay ?? 5000,
            autohide: true,
        });

        toast.show();
    };

    const value: ToastContextType = {
        show,
    };

    return (
        <ToastContext.Provider value={value}>
            {children}
            <Toast ref={toastRef} message={message} resultType={resultType} />
        </ToastContext.Provider>
    );
};

export const ToastContext = createContext<ToastContextType | undefined>(undefined);
