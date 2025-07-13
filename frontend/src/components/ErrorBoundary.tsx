import React from "react";

interface errorBoundaryProps {
    errorUI?: React.ReactNode;
    children: React.ReactNode;
}

interface errorBoundaryState {
    hasError: boolean;
    message: string;
}

class ErrorBoundary extends React.Component<errorBoundaryProps, errorBoundaryState> {
    constructor(props: errorBoundaryProps) {
        super(props);
        this.state = { hasError: false, message: "" };
    }

    static getDerivedStateFromError(error: any) {
        return { hasError: true, message: error };
    }

    componentDidCatch(error: any, errorInfo: any) {
        console.log(error);
    }

    render() {
        if (this.state.hasError === false) return this.props.children;
        if (this.props.errorUI) return this.props.errorUI;
        return <h3>{this.state.message}</h3>;
    }
}

export default ErrorBoundary;
