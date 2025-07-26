import { render, screen } from "@testing-library/react";
import { useForm, FormProvider } from "react-hook-form";
import userEvent from "@testing-library/user-event";
import TextField from "../../components/TextField";

/** Reusable form wrapper for TextField. */
function TestFormWrapper(props: any) {
    const methods = useForm({ defaultValues: { title: "" } });

    return (
        <FormProvider {...methods}>
            <form onSubmit={methods.handleSubmit(() => {})}>
                <TextField {...props} register={methods.register} errors={methods.formState.errors} />
                <button type="submit">Submit</button>
            </form>
        </FormProvider>
    );
}

describe("TextField", () => {
    test("Test 1: Renders label, input, and placeholder", () => {
        render(<TestFormWrapper id="title" label="Title" placeholder="Enter title" required className="mb-3" />);
        expect(screen.getByLabelText(/title/i)).toBeInTheDocument();
        expect(screen.getByPlaceholderText("Enter title")).toBeInTheDocument();
    });

    test("Test 2: Displays asterisk when required", () => {
        render(<TestFormWrapper id="title" label="Title" required />);
        expect(screen.getByText(/title \*/i)).toBeInTheDocument();
    });

    test("Test 3: Shows validation error when left empty", async () => {
        const user = userEvent.setup();
        render(<TestFormWrapper id="title" label="Title" required />);
        await user.click(screen.getByRole("button", { name: /submit/i }));
        expect(screen.getByRole("textbox")).toHaveClass("is-invalid");
        expect(screen.getByText("Title is required")).toBeInTheDocument();
    });

    test("Test 4: Removes error when field is filled", async () => {
        const user = userEvent.setup();
        render(<TestFormWrapper id="title" label="Title" required />);
        const input = screen.getByRole("textbox");

        await user.click(screen.getByRole("button", { name: /submit/i }));
        expect(input).toHaveClass("is-invalid");

        await user.type(input, "Avengers");
        await user.click(screen.getByRole("button", { name: /submit/i }));

        expect(input).not.toHaveClass("is-invalid");
    });

    test("Test 5: Applies custom className", () => {
        render(<TestFormWrapper id="title" label="Title" className="custom-class" />);
        expect(screen.getByRole("textbox").closest("div")).toHaveClass("custom-class");
    });
});
