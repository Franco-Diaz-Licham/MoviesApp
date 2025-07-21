import { LoginFormData } from "../types/user/LoginFormData.type";
import { UserLogin } from "../types/user/UserLogin.type";
import { UserRegister } from "../types/user/UserRegister.type";
import { UserResponse } from "../types/user/UserResponse.type";

/** Maps from API response to form data DTO. */
export function mapResponseToForm(data: UserResponse): LoginFormData {
    return {
        firstName: data.firstName,
        surname: data.surname,
        email: data.email,
        displayName: data.displayName,
    };
}

/** Maps from form data DTO to create DTO. */
export function mapFormToRegister(data: LoginFormData): UserRegister {
    return {
        firstName: data.firstName!,
        surname: data.surname!,
        displayName: data.displayName!,
        email: data.email,
        password: data.password!,
    };
}

/** Maps from form data DTO to update DTO. */
export function mapFormToLogin(data: LoginFormData): UserLogin {
    return {
        email: data.email,
        password: data.password!,
    };
}
