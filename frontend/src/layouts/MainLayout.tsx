import React from "react";
import Footer from "../components/Footer";
import NavBar from "../components/NavBar";

/** Function props. */
interface MainLayoutProps {
    children: React.ReactNode;
}

/** Main page layout. Information wil lbe displayed in the main component. */
const MainLayout = (props: MainLayoutProps) => {
    return (
        <>
            <header className="sticky-top">
                <NavBar />
            </header>
            <main className="container flex-grow-1 py-3">{props.children}</main>
            <Footer />
        </>
    );
};

export default MainLayout;
