import React, { createContext, ReactNode, useContext, useState } from "react";

export interface LoadingContextType {
    loading: boolean;
    setLoading: (value: boolean) => void;
}

export const LoadingContext = createContext<LoadingContextType | undefined>(undefined);

export const LoadingProvider = ({ children }: { children: ReactNode }) => {
    const [loading, setLoading] = useState(false);
    const value: LoadingContextType = {
        loading,
        setLoading,
    };

    return <LoadingContext.Provider value={value}>{children}</LoadingContext.Provider>;
};
