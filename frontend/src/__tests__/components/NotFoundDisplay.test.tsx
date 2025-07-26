import { render, screen } from "@testing-library/react";
import { BrowserRouter } from "react-router-dom";
import NotFoundDisplay from "../../components/NotFoundDisplay";

const renderWithRouter = (ui: React.ReactElement) => {
    return render(<BrowserRouter>{ui}</BrowserRouter>);
};

describe("NotFoundDisplay", () => {
    test("Test 1: Renders heading and message", () => {
        renderWithRouter(<NotFoundDisplay />);
        expect(screen.getByText(/Page Not Found/i)).toBeInTheDocument();
        expect(screen.getByText(/Sorry the page you are looking for/i)).toBeInTheDocument();
    });

    test("Test 2: Renders Go Home button with correct icon and class", () => {
        renderWithRouter(<NotFoundDisplay />);
        const button = screen.getByRole("button", { name: /Go Home/i });
        expect(button).toBeInTheDocument();
        expect(button).toHaveClass("btn", "btn-info");
        expect(screen.getByText(/Go Home/i).querySelector("i")).not.toBeNull();
    });

    test("Test 3: Go Home button links to '/'", () => {
        renderWithRouter(<NotFoundDisplay />);
        const link = screen.getByRole("link", { name: /Go Home/i });
        expect(link).toHaveAttribute("href", "/");
    });
});
