import { render, screen } from "@testing-library/react";
import Toast from "../../components/Toast";

describe("Toast component", () => {
    const baseProps = {
        message: "This is a test toast",
        resultType: "success",
    };

    test("Test 1: Renders the toast message", () => {
        render(<Toast {...baseProps} />);
        expect(screen.getByText(/this is a test toast/i)).toBeInTheDocument();
    });

    test("Test 2: Has the correct Bootstrap background class", () => {
        render(<Toast {...baseProps} />);
        const toast = screen.getByRole("alert");
        expect(toast).toHaveClass("text-bg-success");
    });

    test("Test 3: Has the correct aria attributes", () => {
        render(<Toast {...baseProps} />);
        const toast = screen.getByRole("alert");
        expect(toast).toHaveAttribute("aria-live", "assertive");
        expect(toast).toHaveAttribute("aria-atomic", "true");
    });

    test("Test 4: Renders the close button", () => {
        render(<Toast {...baseProps} />);
        const closeBtn = screen.getByRole("button", { name: /close/i });
        expect(closeBtn).toBeInTheDocument();
        expect(closeBtn).toHaveClass("btn-close-white");
    });
});
