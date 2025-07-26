import { render, screen } from "@testing-library/react";
import TextField from "../../components/TextField";
import type { FieldErrors, UseFormRegisterReturn } from "react-hook-form";

describe("TextField", () => {
    const baseProps = {
        id: "title",
        label: "Title",
        type: "text",
        required: true,
        className: "mb-3",
        placeholder: "Enter title",
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

    test("Test 1: Renders label and input", () => {
        render(<TextField {...baseProps} />);
        expect(screen.getByLabelText(/title/i)).toBeInTheDocument();
        expect(screen.getByRole("textbox")).toBeInTheDocument();
    });

    test("Test 2: Displays asterisk if required", () => {
        render(<TextField {...baseProps} />);
        expect(screen.getByText(/title \*/i)).toBeInTheDocument();
    });

    test("Test 3: Displays placeholder", () => {
        render(<TextField {...baseProps} />);
        expect(screen.getByPlaceholderText("Enter title")).toBeInTheDocument();
    });

    test("Test 4: Applies is-invalid class when error exists", () => {
        const propsWithError = {
            ...baseProps,
            errors: {
                title: {
                    type: "required",
                    message: "Title is required",
                },
            } as FieldErrors,
        };

        render(<TextField {...propsWithError} />);
        const input = screen.getByRole("textbox");
        expect(input).toHaveClass("is-invalid");
        expect(screen.getByText("Title is required")).toBeInTheDocument();
    });

    test("Test 5: Does not apply is-invalid class when no error", () => {
        render(<TextField {...baseProps} />);
        const input = screen.getByRole("textbox");
        expect(input).not.toHaveClass("is-invalid");
    });

    test("Test 6: Uses provided className", () => {
        render(<TextField {...baseProps} />);
        expect(screen.getByRole("textbox").closest("div")).toHaveClass("mb-3");
    });
});
