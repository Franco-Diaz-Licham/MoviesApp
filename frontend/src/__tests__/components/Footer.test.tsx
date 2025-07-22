import { render, screen } from "@testing-library/react";
import Footer from "../../components/Footer";

describe("Footer", () => {
    test("renders the footer text", () => {
        render(<Footer />);
        const footerText = screen.getByText(/movies app, by franco diaz/i);
        expect(footerText).toBeInTheDocument();
    });

    test("has correct classes", () => {
        render(<Footer />);
        const footer = screen.getByText(/movies app, by franco diaz/i);
        expect(footer).toHaveClass("text-center", "fw-bold", "bg-dark", "text-white", "py-3");
    });
});
