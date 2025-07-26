import { render, screen, fireEvent } from "@testing-library/react";
import BadgeField from "../../components/Badgefield";

describe("BadgeField", () => {
    test("Test 1: Renders badge with default primary color", () => {
        render(<BadgeField name="Admin" />);
        const badge = screen.getByText(/admin/i);
        expect(badge).toHaveClass("bg-primary");
    });

    test("Test 2: Renders badge with custom color", () => {
        render(<BadgeField name="VIP" colour="danger" />);
        const badge = screen.getByText(/vip/i);
        expect(badge).toHaveClass("bg-danger");
    });

    test("Test 3: Renders remove button if onRemove is provided", () => {
        render(<BadgeField name="removable" onRemove={() => {}} />);
        const closeBtn = screen.getByRole("button", { name: /remove/i });
        expect(closeBtn).toBeInTheDocument();
    });

    test("Test 4: Calls onRemove when close button is clicked", () => {
        const mockRemove = jest.fn();
        render(<BadgeField name="Closable" onRemove={mockRemove} />);
        const closeBtn = screen.getByRole("button", { name: /remove/i });
        fireEvent.click(closeBtn);
        expect(mockRemove).toHaveBeenCalled();
    });
});
