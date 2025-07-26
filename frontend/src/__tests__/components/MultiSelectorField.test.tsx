jest.mock("../../components/Badgefield", () => ({
    __esModule: true,
    default: ({ name, onRemove }: any) => (
        <div data-testid="badge" onClick={onRemove}>
            {name}
        </div>
    ),
}));

import { render, screen, fireEvent } from "@testing-library/react";
import { Controller, FormProvider, useForm } from "react-hook-form";
import userEvent from "@testing-library/user-event";
import MultiSelectField from "../../components/MultiSelectorField";

const genres = [
    { id: 1, name: "Action" },
    { id: 2, name: "Drama" },
];

/** Integration wrapper using Controller and FormProvider */
function TestWrapper() {
    const methods = useForm({
        defaultValues: {
            genres: [],
        },
    });

    return (
        <FormProvider {...methods}>
            <form onSubmit={methods.handleSubmit(() => {})}>
                <Controller
                    name="genres"
                    control={methods.control}
                    rules={{
                        validate: (value) => value.length > 0 || "At least one genre is required",
                    }}
                    render={({ field }) => (
                        <MultiSelectField
                            {...field}
                            id="genres"
                            label="Genres"
                            values={genres}
                            value={(g) => g.name}
                            valueId={(g) => g.id}
                            errors={methods.formState.errors}
                            watch={methods.watch}
                            setValue={methods.setValue}
                            trigger={methods.trigger}
                            placeholder="Select genres"
                            badgeColour="primary"
                            showLabel
                            required
                        />
                    )}
                />
                <button type="submit">Submit</button>
            </form>
        </FormProvider>
    );
}

describe("MultiSelectField (Integration)", () => {
    test("Test 1: Renders correctly with label and options", () => {
        render(<TestWrapper />);
        expect(screen.getByLabelText(/genres/i)).toBeInTheDocument();
        expect(screen.getByText("-- Select genres --")).toBeInTheDocument();
        expect(screen.getByText("Action")).toBeInTheDocument();
        expect(screen.getByText("Drama")).toBeInTheDocument();
    });

    test("Test 2: Adds a selected genre on change", () => {
        render(<TestWrapper />);
        const select = screen.getByLabelText(/genres/i);
        fireEvent.change(select, { target: { value: "1" } });
        expect(screen.getByTestId("badge")).toHaveTextContent("Action");
    });

    test("Test 3: Prevents duplicate genre selections", () => {
        render(<TestWrapper />);
        const select = screen.getByLabelText(/genres/i);
        fireEvent.change(select, { target: { value: "1" } });
        fireEvent.change(select, { target: { value: "1" } });
        const badges = screen.getAllByTestId("badge");
        expect(badges.length).toBe(1);
    });

    test("Test 4: Removes a genre when badge is clicked", () => {
        render(<TestWrapper />);
        const select = screen.getByLabelText(/genres/i);
        fireEvent.change(select, { target: { value: "1" } });
        fireEvent.change(select, { target: { value: "2" } });

        let badges = screen.getAllByTestId("badge");
        expect(badges.length).toBe(2);

        fireEvent.click(badges[0]); // remove "Action"
        badges = screen.getAllByTestId("badge");
        expect(badges.length).toBe(1);
        expect(badges[0]).toHaveTextContent("Drama");
    });

    test("Test 5: Shows validation error on submit if none selected", async () => {
        const user = userEvent.setup();
        render(<TestWrapper />);
        await user.click(screen.getByRole("button", { name: /submit/i }));
        expect(await screen.findByText(/at least one genre is required/i)).toBeInTheDocument();
    });

    test("Test 6: Passes validation when at least one genre is selected", async () => {
        const user = userEvent.setup();
        render(<TestWrapper />);
        const select = screen.getByLabelText(/genres/i);
        await user.selectOptions(select, "1");
        await user.click(screen.getByRole("button", { name: /submit/i }));

        expect(screen.queryByText(/at least one genre is required/i)).not.toBeInTheDocument();
    });
});
