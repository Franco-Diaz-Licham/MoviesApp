import { LoginFormData } from "../types/user/LoginFormData.type";
import { UserLogin } from "../types/user/UserLogin.type";
import { UserRegister } from "../types/user/UserRegister.type";
import { UserResponse } from "../types/user/UserResponse.type";
import { UserUpdate } from "../types/user/UserUpdate.type";


export function mapResponseToForm(data: UserResponse): LoginFormData {
    return {
        firstName: data.firstName,
        surname: data.surname,
        email: data.email,
        displayName: data.displayName,
    };
}

export function mapFormToRegister(data: LoginFormData): UserRegister {
    return {
        firstName: data.firstName!,
        surname: data.surname!,
        displayName: data.displayName!,
        email: data.email,
        password: data.password!,
    };
}

export function mapFormToLogin(data: LoginFormData): UserLogin {
    return {
        email: data.email,
        password: data.password!,
    };
}
