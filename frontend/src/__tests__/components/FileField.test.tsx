// Setup mocks.
jest.mock("../../assets/genericUser.jpg", () => "mocked/genericUser.jpg");

// Import modules
import { render, screen } from "@testing-library/react";
import { useForm, FormProvider } from "react-hook-form";
import FileField from "../../components/FileField";

/** Wrap field with a FromProvider context. Return a functional component. */
function TestFormWrapper(props: any) {
    const methods = useForm();
    return (
        <FormProvider {...methods}>
            <form onSubmit={methods.handleSubmit(() => {})}>
                <FileField {...props} register={methods.register} errors={methods.formState.errors} />
                <button type="submit">Submit</button>
            </form>
        </FormProvider>
    );
}

describe("FileField", () => {
    afterEach(() => jest.clearAllMocks());

    // Test 1
    test("renders label and default image", () => {
        render(<TestFormWrapper id="photo" label="Profile Image" height={100} width={100} errors={{}} />);
        expect(screen.getByLabelText(/profile image/i)).toBeInTheDocument();
        const img = screen.getByAltText(/preview/i) as HTMLImageElement;
        expect(img).toBeInTheDocument();
        expect(img.src).toMatch(/genericUser/);
    });

    // Test 2
    test("renders with provided imageUrl", () => {
        const customUrl = "https://www.cloudinary.com/100";
        render(<TestFormWrapper id="photo" label="Profile Image" height={100} width={100} imageUrl={customUrl} errors={{}} />);
        const img = screen.getByAltText(/preview/i) as HTMLImageElement;
        expect(img.src).toBe(customUrl);
    });
});
