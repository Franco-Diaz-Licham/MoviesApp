import { useForm } from "react-hook-form";
import logo from "../../assets/logo.png";
import { LoginFormData } from "../../types/user/LoginFormData.type";
import TextField from "../../components/TextField";

/** Function props. */
interface LoginFormProps {
    onRegister: () => void;
    onSubmit:  (data: LoginFormData) => void;
}

/** Login form component. */
export default function LoginForm(props: LoginFormProps) {
    const {
        register,
        handleSubmit,
        formState: { errors, isSubmitting },
    } = useForm<LoginFormData>();

    /** Gets the login button content. */
    const getLoginContent = () => {
        if (!isSubmitting) return "Login";
        return (
            <>
                <span role="status">Logging in...</span>
                <span className="spinner-border spinner-border-sm ms-3" aria-hidden="true"></span>
            </>
        );
    };

    return (
        <form onSubmit={handleSubmit(props.onSubmit)} className="p-5 shadow rounded-4 bg-white">
            <img src={logo} alt="Company logo" className="mb-4 mx-3" height={140} />
            <h2 className="mb-4 text-center">Login</h2>
            <TextField className="mb-3" id="email" label="Email" type="email" placeholder="empty" required register={register} errors={errors} />
            <TextField className="mb-4" id="password" label="Password" type="password" placeholder="empty" required register={register} errors={errors} />
            <div className="text-center">
                <button type="submit" className="btn btn-primary w-100">
                    {getLoginContent()}
                </button>
                <a className="mt-3 d-block fw-bold" style={{ cursor: "pointer" }} onClick={() => props.onRegister()}>
                    Register
                </a>
            </div>
        </form>
    );
}
