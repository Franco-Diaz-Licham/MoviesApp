import { createContext, useRef, useState } from "react";
import { Toast as BootstrapToast } from "bootstrap";
import Toast from "../components/Toast";

interface ToastContextType {
    show: (msg: string, options?: ToastOptions) => void;
}

/** Options for toaster display configuration. */
interface ToastOptions {
    delay?: number;
    result?: string;
}

/** Toaster provider. Shows whenever an API request is submitted and displays error messages. */
export const ToastProvider = ({ children }: { children: React.ReactNode }) => {
    /** Toaster component to be rendered based on request status. */
    const toastRef = useRef<HTMLDivElement>(null);
    const [message, setMessage] = useState("");
    const [resultType, setResultType] = useState("");

    /** Method will display the toast. */
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

    return (
        <ToastContext.Provider value={{ show }}>
            {children}
            <Toast ref={toastRef} message={message} resultType={resultType} />
        </ToastContext.Provider>
    );
};

export const ToastContext = createContext<ToastContextType | undefined>(undefined);
