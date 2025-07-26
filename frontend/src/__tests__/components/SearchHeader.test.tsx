import { render, screen, fireEvent } from "@testing-library/react";
import SearchHeader from "../../components/SearchHeader";
import { BrowserRouter } from "react-router-dom";

describe("SearchHeader", () => {
    const values = [
        { id: 1, name: "Alpha" },
        { id: 2, name: "Beta" },
        { id: 3, name: "Gamma" },
    ];

    const onSearch = jest.fn();
    const props = {
        values,
        to: "items",
        title: "Test Items",
        onSearch,
        searchKey: (item: any) => item.name,
    };

    const renderComponent = () =>
        render(
            <BrowserRouter>
                <SearchHeader {...props} />
            </BrowserRouter>
        );

    test("Test 1: renders title and input field", () => {
        renderComponent();
        expect(screen.getByText("Test Items")).toBeInTheDocument();
        expect(screen.getByPlaceholderText("Search...")).toBeInTheDocument();
    });

    test("Test 2: calls onSearch with filtered results", () => {
        renderComponent();
        const input = screen.getByPlaceholderText("Search...");
        fireEvent.change(input, { target: { value: "t" } });
        expect(onSearch).toHaveBeenCalledWith([{ id: 2, name: "Beta" }]);
    });

    test("Test 3: renders 'Add' button with correct link", () => {
        renderComponent();
        const link = screen.getByRole("link", { name: /add/i });
        expect(link).toHaveAttribute("href", "/items/");
    });

    test("Test 4: search is case-insensitive", () => {
        renderComponent();
        const input = screen.getByPlaceholderText("Search...");
        fireEvent.change(input, { target: { value: "GAMMA" } });
        expect(onSearch).toHaveBeenCalledWith([{ id: 3, name: "Gamma" }]);
    });
});
