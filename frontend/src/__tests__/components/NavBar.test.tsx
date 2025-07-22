// Setup mocks
jest.mock("../../assets/logo.png", () => "mocked/logo.png");
jest.mock("../../hooks/useAuth", () => ({
    useAuth: jest.fn(),
}));

// Import modules
import { render, screen, fireEvent } from "@testing-library/react";
import NavBar from "../../components/NavBar";
import { useAuth } from "../../hooks/useAuth";
import { MemoryRouter } from "react-router-dom";

describe("NavBar", () => {
    afterEach(() => jest.clearAllMocks());

    // Test 1
    test("renders all navigation links", () => {
        (useAuth as jest.Mock).mockReturnValue({ currentUser: null });
        render(<NavBar />, { wrapper: MemoryRouter });

        expect(screen.getByRole("img", { name: /logo/i })).toHaveAttribute("src", "mocked/logo.png");
        expect(screen.getByRole("link", { name: /movies/i })).toBeInTheDocument();
        expect(screen.getByRole("link", { name: /actors/i })).toBeInTheDocument();
        expect(screen.getByRole("link", { name: /theatres/i })).toBeInTheDocument();
        expect(screen.getByRole("link", { name: /genres/i })).toBeInTheDocument();
    });

    // Test 2
    test("shows Login when user is not logged in", () => {
        (useAuth as jest.Mock).mockReturnValue({ currentUser: null });
        render(<NavBar />, { wrapper: MemoryRouter });

        const loginLink = screen.getByRole("link", { name: /login/i });
        expect(loginLink).toBeInTheDocument();
        expect(loginLink).toHaveAttribute("href", "/login");
    });

    // Test 3
    test("shows Logout when user is logged in", () => {
        const logoutMock = jest.fn();
        (useAuth as jest.Mock).mockReturnValue({ currentUser: { name: "Franco" }, logout: logoutMock });
        render(<NavBar />, { wrapper: MemoryRouter });

        const logoutLink = screen.getByRole("link", { name: /logout/i });
        expect(logoutLink).toBeInTheDocument();
        fireEvent.click(logoutLink);
        expect(logoutMock).toHaveBeenCalled();
    });

    // Test 4
    test("toggles navbar collapse", () => {
        (useAuth as jest.Mock).mockReturnValue({ currentUser: null });
        render(<NavBar />, { wrapper: MemoryRouter });
        const toggler = screen.getByRole("button", { name: /toggle navigation/i });
        const navCollapse = screen.getByTestId("navbar-collapse");

        // Initially open
        expect(navCollapse.className).toContain("collapse");

        // Toggle button clicked
        fireEvent.click(toggler);
        expect(navCollapse.className).toContain("collapsed");
    });
});
