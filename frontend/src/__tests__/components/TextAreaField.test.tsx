import { render, screen } from "@testing-library/react";
import TextAreaField from "../../components/TextAreaField";
import type { FieldErrors, UseFormRegisterReturn } from "react-hook-form";

describe("TextAreaField", () => {
    const baseProps = {
        id: "description",
        label: "Description",
        rows: 5,
        required: true,
        className: "mb-3",
        placeholder: "Enter description...",
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

    test("Test 1: Renders label and textarea", () => {
        render(<TextAreaField {...baseProps} />);
        expect(screen.getByLabelText(/description/i)).toBeInTheDocument();
        expect(screen.getByRole("textbox")).toBeInTheDocument();
    });

    test("Test 2: Shows asterisk when required", () => {
        render(<TextAreaField {...baseProps} />);
        expect(screen.getByText(/description \*/i)).toBeInTheDocument();
    });

    test("Test 3: Shows placeholder if provided", () => {
        render(<TextAreaField {...baseProps} />);
        expect(screen.getByPlaceholderText("Enter description...")).toBeInTheDocument();
    });

    test("Test 4: Applies is-invalid class when error exists", () => {
        const propsWithError = {
            ...baseProps,
            errors: {
                description: {
                    type: "required",
                    message: "Description is required",
                },
            } as FieldErrors,
        };

        render(<TextAreaField {...propsWithError} />);
        expect(screen.getByRole("textbox")).toHaveClass("is-invalid");
        expect(screen.getByText("Description is required")).toBeInTheDocument();
    });

    test("Test 5: Does not apply is-invalid class when no error", () => {
        render(<TextAreaField {...baseProps} />);
        expect(screen.getByRole("textbox")).not.toHaveClass("is-invalid");
    });

    test("Test 6: Uses provided className on wrapper", () => {
        render(<TextAreaField {...baseProps} />);
        expect(screen.getByRole("textbox").closest("div")).toHaveClass("mb-3");
    });
});
