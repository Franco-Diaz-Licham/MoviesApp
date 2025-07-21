import { forwardRef } from "react";

/** Function props. */
interface ToastProps {
    message: string;
    resultType: string;
}

/** Toast component. */
const Toast = forwardRef<HTMLDivElement, ToastProps>((props: ToastProps, ref) => {
    return (
        <div ref={ref} className={`toast position-fixed bottom-0 end-0 m-4 text-bg-${props.resultType}`} role="alert" aria-live="assertive" aria-atomic="true">
            <div className="d-flex">
                <div className="toast-body">{props.message}</div>
                <button type="button" className="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
            </div>
        </div>
    );
});

export default Toast;
