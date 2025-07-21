import { useState } from "react";
import LoginForm from "../features/auth/LoginForm";
import css from "../styles/Login.module.css";
import { LoginFormData } from "../types/user/LoginFormData.type";
import RegisterForm from "../features/auth/RegisterForm";
import { useAuth } from "../hooks/useAuth";
import { mapFormToLogin, mapFormToRegister } from "../utils/UserMapper";
import { useNavigate } from "react-router-dom";

export default function Login() {
    const navigate = useNavigate();
    const [loginStatus, setLoginStatus] = useState(true);
    const { login, register } = useAuth();

    /** Method which handles submission. */
    const handleSubmitChanged = async (data: LoginFormData) => {
        let success: boolean = false;
        if (loginStatus) success = await logUser(data);
        else success = await registerUser(data);
        if (success) navigate("/");
    };

    /**Method which calls login authentication method. */
    const logUser = async (data: LoginFormData): Promise<boolean> => {
        var model = mapFormToLogin(data);
        return await login(model);
    };

    /** MEthod which calls register authentication method. */
    const registerUser = async (data: LoginFormData): Promise<boolean> => {
        var model = mapFormToRegister(data);
        return await register(model);
    };

    /** Method which archestrates whether the login or register forms. */
    const getForm = () => {
        if (loginStatus) return <LoginForm onRegister={() => setLoginStatus(false)} onSubmit={handleSubmitChanged} />;
        return <RegisterForm onCancel={() => setLoginStatus(true)} onSubmit={handleSubmitChanged} />;
    };

    return <div className={css.login}>{getForm()}</div>;
}
