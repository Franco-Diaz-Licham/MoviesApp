import { render, screen } from "@testing-library/react";
import { SwitchField } from "../../components/SwitchField";
import { FieldError, FieldErrors } from "react-hook-form";

describe("SwitchField", () => {
    const baseProps = {
        id: "active",
        label: "Is Active",
        required: true,
        className: "mb-3",
        errors: {} as FieldErrors,
        register: jest.fn((name) => ({
            name,
            onChange: jest.fn(),
            onBlur: jest.fn(),
            ref: jest.fn(),
        })),
    };

    beforeEach(() => {
        jest.clearAllMocks();
    });

    test("Test 1: Renders the label and switch input", () => {
        render(<SwitchField {...baseProps} />);
        expect(screen.getByLabelText(/is active/i)).toBeInTheDocument();
        expect(screen.getByRole("checkbox")).toBeInTheDocument();
    });

    test("Test 2: Applies 'is-invalid' class if there is an error", () => {
        const propsWithError = {
            ...baseProps,
            errors: {
                active: {
                    type: "manual",
                    message: "This field is required",
                } satisfies FieldError,
            },
        };

        render(<SwitchField {...propsWithError} />);
        const input = screen.getByRole("checkbox");
        expect(input).toHaveClass("is-invalid");
        expect(screen.getByText("This field is required")).toBeInTheDocument();
    });

    test("Test 3: Does not apply 'is-invalid' when there's no error", () => {
        render(<SwitchField {...baseProps} />);
        const input = screen.getByRole("checkbox");
        expect(input).not.toHaveClass("is-invalid");
    });

    test("Test 4: Includes asterisk in label when required", () => {
        render(<SwitchField {...baseProps} />);
        expect(screen.getByText(/is active \*/i)).toBeInTheDocument();
    });

    test("Test 5: Uses the provided className wrapper", () => {
        render(<SwitchField {...baseProps} />);
        expect(screen.getByRole("checkbox").closest("div")?.parentElement).toHaveClass("mb-3");
    });
});
