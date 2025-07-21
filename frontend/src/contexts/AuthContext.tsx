import { createContext, ReactNode, useEffect, useState } from "react";
import { UserResponse } from "../types/user/UserResponse.type";
import { getUser, loginUser, registerUser } from "../api/services/userService";
import { UserLogin } from "../types/user/UserLogin.type";
import { UserRegister } from "../types/user/UserRegister.type";

export const AuthContext = createContext<AuthContextType | undefined>(undefined);

/** Authentication state provider. */
export const AuthProvider = ({ children }: { children: ReactNode }) => {
    const [currentUser, setCurrentUser] = useState<UserResponse | null>(null);

    /** Method which register a user. */
    const register = async (model: UserRegister): Promise<boolean> => {
        const user = await registerUser(model);
        if (!user) return false;
        setCurrentUser(user);
        return true;
    };

    /** Method which logins a user. */
    const login = async (model: UserLogin): Promise<boolean> => {
        const user = await loginUser(model);
        if (!user) return false;
        setCurrentUser(user);
        return true;
    };

    /** Method which logouts a user. */
    const logout = async () => {
        localStorage.removeItem("token");
        setCurrentUser(null);
    };

    /** Fetches user information application load. */
    const loadUser = async () => {
        const token = localStorage.getItem("token");
        if (token) {
            const user = await getUser();
            if (user) setCurrentUser(user);
        }
    };

    useEffect(() => {
        loadUser();
    }, []);

    const value: AuthContextType = {
        currentUser,
        register,
        login,
        logout,
    };

    return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};

/** Context type interface */
interface AuthContextType {
    currentUser: UserResponse | null;
    register: (model: UserRegister) => Promise<boolean>;
    login: (model: UserLogin) => Promise<boolean>;
    logout: () => Promise<void> | void;
}
