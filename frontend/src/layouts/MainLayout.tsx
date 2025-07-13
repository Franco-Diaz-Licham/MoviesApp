import React from "react";
import Footer from "../components/Footer";
import NavBar from "../components/NavBar";

/** Function props. */
interface MainLayoutProps {
    children: React.ReactNode;
}

const MainLayout = (props: MainLayoutProps) => {
    return (
        <>
            <header>
                <NavBar />
            </header>
            <main className="container flex-grow-1">{props.children}</main>
            <Footer />
        </>
    );
};

export default MainLayout;
