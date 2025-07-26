import { render, screen } from "@testing-library/react";
import { useForm, FormProvider } from "react-hook-form";
import userEvent from "@testing-library/user-event";
import TextAreaField from "../../components/TextAreaField";

/** Wrap TextAreaField inside a real form using react-hook-form context */
function TestFormWrapper(props: any) {
    const methods = useForm({ defaultValues: { [props.id]: "" } });

    return (
        <FormProvider {...methods}>
            <form onSubmit={methods.handleSubmit(() => {})}>
                <TextAreaField {...props} register={methods.register} errors={methods.formState.errors} />
                <button type="submit">Submit</button>
            </form>
        </FormProvider>
    );
}

describe("TextAreaField", () => {
    test("Test 1: Renders label and textarea", () => {
        render(<TestFormWrapper id="description" label="Description" required rows={5} placeholder="Enter description..." className="mb-3" />);
        expect(screen.getByLabelText(/description/i)).toBeInTheDocument();
        expect(screen.getByPlaceholderText("Enter description...")).toBeInTheDocument();
    });

    test("Test 2: Displays asterisk if required", () => {
        render(<TestFormWrapper id="description" label="Description" required />);
        expect(screen.getByText(/description \*/i)).toBeInTheDocument();
    });

    test("Test 3: Shows validation error when empty and submitted", async () => {
        const user = userEvent.setup();
        render(<TestFormWrapper id="description" label="Description" required />);
        await user.click(screen.getByRole("button", { name: /submit/i }));
        expect(screen.getByRole("textbox")).toHaveClass("is-invalid");
        expect(screen.getByText("Description is required")).toBeInTheDocument();
    });

    test("Test 4: Accepts valid input and clears error", async () => {
        const user = userEvent.setup();
        render(<TestFormWrapper id="description" label="Description" required />);
        const input = screen.getByRole("textbox");

        await user.click(screen.getByRole("button", { name: /submit/i }));
        expect(input).toHaveClass("is-invalid");

        await user.type(input, "This is valid.");
        await user.click(screen.getByRole("button", { name: /submit/i }));

        expect(input).not.toHaveClass("is-invalid");
    });

    test("Test 5: Wrapper div uses provided className", () => {
        render(<TestFormWrapper id="description" label="Description" className="mb-3" />);
        expect(screen.getByRole("textbox").closest("div")).toHaveClass("mb-3");
    });
});
