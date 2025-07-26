import { render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { useForm, FormProvider } from "react-hook-form";
import { SwitchField } from "../../components/SwitchField";

/** Wrap SwitchField inside react-hook-form context */
function TestFormWrapper(props: any) {
    const methods = useForm({ defaultValues: { [props.id]: false } });

    return (
        <FormProvider {...methods}>
            <form onSubmit={methods.handleSubmit(() => {})}>
                <SwitchField {...props} register={methods.register} errors={methods.formState.errors} />
                <button type="submit">Submit</button>
            </form>
        </FormProvider>
    );
}

describe("SwitchField", () => {
    test("Test 1: Renders the label and input", () => {
        render(<TestFormWrapper id="active" label="Is Active" required className="mb-3" />);
        expect(screen.getByLabelText(/is active/i)).toBeInTheDocument();
        expect(screen.getByRole("checkbox")).toBeInTheDocument();
    });

    test("Test 2: Applies 'is-invalid' when required and not checked on submit", async () => {
        const user = userEvent.setup();
        render(<TestFormWrapper id="active" label="Is Active" required />);
        await user.click(screen.getByRole("button", { name: /submit/i }));

        expect(screen.getByRole("checkbox")).toHaveClass("is-invalid");
        expect(screen.getByText("Is Active is required")).toBeInTheDocument();
    });

    test("Test 3: Does not apply 'is-invalid' when checked", async () => {
        const user = userEvent.setup();
        render(<TestFormWrapper id="active" label="Is Active" required />);
        const input = screen.getByRole("checkbox");
        await user.click(input);
        await user.click(screen.getByRole("button", { name: /submit/i }));

        expect(input).not.toHaveClass("is-invalid");
    });

    test("Test 4: Includes asterisk in label if required", () => {
        render(<TestFormWrapper id="active" label="Is Active" required />);
        expect(screen.getByText(/is active \*/i)).toBeInTheDocument();
    });

    test("Test 5: Uses the provided className wrapper", () => {
        render(<TestFormWrapper id="active" label="Is Active" required className="mb-3" />);
        expect(screen.getByRole("checkbox").closest("div")?.parentElement).toHaveClass("mb-3");
    });
});
