jest.mock("../../components/Badgefield", () => ({ name, onRemove }: any) => (
    <div data-testid="badge" onClick={onRemove}>
        {name}
    </div>
));

import { render, screen, fireEvent } from "@testing-library/react";
import MultiSelectField from "../../components/MultiSelectorField";
import type { FieldError } from "react-hook-form";

describe("MultiSelectField", () => {
    const genres = [
        { id: 1, name: "Action" },
        { id: 2, name: "Drama" },
    ];

    const baseProps = {
        id: "genres",
        label: "Genres",
        placeholder: "Select Genre",
        values: genres,
        showLabel: true,
        required: true,
        badgeColour: "primary",
        value: (g: any) => g.name,
        valueId: (g: any) => g.id,
        errors: {},
        watch: jest.fn(),
        setValue: jest.fn(),
        trigger: jest.fn(),
    };

    test("Test 1: Renders label and options", () => {
        baseProps.watch.mockReturnValue([]);
        render(<MultiSelectField {...baseProps} />);
        expect(screen.getByLabelText(/genres/i)).toBeInTheDocument();
        expect(screen.getByText("-- Select Genre --")).toBeInTheDocument();
        expect(screen.getByText("Action")).toBeInTheDocument();
        expect(screen.getByText("Drama")).toBeInTheDocument();
    });

    test("Test 2: Adds selected item on change", () => {
        baseProps.watch.mockReturnValue([]);
        render(<MultiSelectField {...baseProps} />);
        fireEvent.change(screen.getByLabelText(/genres/i), { target: { value: "1" } });

        expect(baseProps.setValue).toHaveBeenCalledWith("genres", [1]);
        expect(baseProps.trigger).toHaveBeenCalledWith("genres");
    });

    test("Test 3: Does not add duplicate values", () => {
        baseProps.watch.mockReturnValue([1]);
        render(<MultiSelectField {...baseProps} />);
        fireEvent.change(screen.getByLabelText(/genres/i), { target: { value: "1" } });

        expect(baseProps.setValue).not.toHaveBeenCalledWith("genres", expect.arrayContaining([1, 1]));
    });

    test("Test 4: Removes a badge when clicked", () => {
        baseProps.watch.mockReturnValue([1, 2]);
        render(<MultiSelectField {...baseProps} />);
        const badges = screen.getAllByTestId("badge");
        expect(badges.length).toBe(2);
        fireEvent.click(badges[0]);

        expect(baseProps.setValue).toHaveBeenCalledWith("genres", [2]);
        expect(baseProps.trigger).toHaveBeenCalledWith("genres");
    });

    test("Test 5: Displays validation error", () => {
        const propsWithError = {
            ...baseProps,
            id: "genres",
            errors: {
                genres: { message: "Selection is required" } as FieldError,
            },
            watch: jest.fn().mockReturnValue([]),
        };

        render(<MultiSelectField {...propsWithError} />);
        expect(screen.getByText("Selection is required")).toBeInTheDocument();
    });
});
