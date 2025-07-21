import { useForm } from "react-hook-form";
import logo from "../../assets/logo.png";
import TextField from "../../components/TextField";
import { LoginFormData } from "../../types/user/LoginFormData.type";

/** Function props. */
interface RegisterFormProps {
    onCancel: () => void;
    onSubmit: (data: LoginFormData) => void;
}

/** Register form component. */
export default function RegisterForm(props: RegisterFormProps) {
    const {
        register,
        handleSubmit,
        formState: { errors, isSubmitting },
    } = useForm<LoginFormData>();

    /** Gets the login button content. */
    const getSignUpContent = () => {
        if (!isSubmitting) return "Sign Up";
        return (
            <>
                <span role="status">Creating Account...</span>
                <span className="spinner-border spinner-border-sm ms-3" aria-hidden="true"></span>
            </>
        );
    };

    return (
        <form onSubmit={handleSubmit(props.onSubmit)} className="p-5 m-1 shadow rounded-4 bg-white">
            <img src={logo} alt="Company logo" className="mb-4 mx-3" height={140} />
            <h2 className="mb-4 text-center">Create Account</h2>
            <TextField className="mb-3" id="firstName" label="First Name" placeholder="empty" required register={register} errors={errors} />
            <TextField className="mb-3" id="surname" label="Surname" placeholder="empty" required register={register} errors={errors} />
            <TextField className="mb-3" id="displayName" label="Display Name" placeholder="empty" required register={register} errors={errors} />
            <TextField className="mb-3" id="email" label="Email" type="email" placeholder="empty" required register={register} errors={errors} />
            <TextField className="mb-4" id="password" label="Password" type="password" placeholder="empty" required register={register} errors={errors} />
            <div className="text-center">
                <button type="submit" className="btn btn-success w-100" disabled={isSubmitting}>
                    {getSignUpContent()}
                </button>
                <a className="mt-3 d-block fw-bold text-danger" style={{ cursor: "pointer" }} onClick={() => props.onCancel()}>
                    Cancel
                </a>
            </div>
        </form>
    );
}
