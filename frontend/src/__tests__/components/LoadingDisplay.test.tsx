// Mock files and hooks
jest.mock("../../assets/loading.gif", () => "mocked/loading.gif");
jest.mock("../../hooks/useLoading", () => ({
    useLoading: jest.fn(),
}));

// Import modules
import { render, screen } from "@testing-library/react";
import LoadingDisplay from "../../components/LoadingDisplay";
import { useLoading } from "../../hooks/useLoading";

describe("LoadingDisplay", () => {
    // Test 1
    test("renders loading component when loading is true", () => {
        (useLoading as jest.Mock).mockReturnValue({ loading: true });
        render(<LoadingDisplay />);

        const overlay = screen.getByRole("img", { name: /loading/i });
        expect(overlay).toBeInTheDocument();
        expect(overlay).toHaveAttribute("src", "mocked/loading.gif");
    });

    // Test 2 
    test("renders nothing when loading is false", () => {
        (useLoading as jest.Mock).mockReturnValue({ loading: false });
        const { container } = render(<LoadingDisplay />);
        expect(container).toBeEmptyDOMElement();
    });
});
