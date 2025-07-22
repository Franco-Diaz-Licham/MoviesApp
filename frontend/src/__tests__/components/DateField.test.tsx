import { render, screen } from "@testing-library/react";
import { useForm, FormProvider } from "react-hook-form";
import userEvent from "@testing-library/user-event";
import DateField from "../../components/DateField";

/** Wrap field with a FromProvider context. Return a functional component. */
function TestFormWrapper(props: any) {
    const methods = useForm();
    return (
        <FormProvider {...methods}>
            <form onSubmit={methods.handleSubmit(() => {})}>
                <DateField {...props} register={methods.register} errors={methods.formState.errors} />
                <button type="submit">Submit</button>
            </form>
        </FormProvider>
    );
}

describe("DateField", () => {
    test("renders with label", () => {
        render(<TestFormWrapper id="dob" label="Date of Birth" errors={{}} />);
        expect(screen.getByLabelText(/date of birth/i)).toBeInTheDocument();
    });

    test("shows validation error when required", async () => {
        const user = userEvent.setup();
        render(<TestFormWrapper id="startDate" label="Start Date" required={true} errors={{}} />);
        await user.click(screen.getByRole("button", { name: /submit/i }));
        expect(screen.getByLabelText(/start date/i)).toHaveClass("is-invalid");
    });

    test("does not show error when filled correctly", async () => {
        const user = userEvent.setup();
        render(<TestFormWrapper id="startDate" label="Start Date" required={true} errors={{}} />);
        const input = screen.getByLabelText(/start date/i);
        await user.type(input, "2025-07-22");
        await user.click(screen.getByRole("button", { name: /submit/i }));
        expect(input).not.toHaveClass("is-invalid");
    });
});
