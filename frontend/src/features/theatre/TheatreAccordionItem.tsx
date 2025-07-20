import { useState } from "react";
import MapField from "../../components/MapField";
import { TheatreResponse } from "../../types/theatre/TheatreResponse.type";
import { Link } from "react-router-dom";

interface TheatreAccordionItemProps {
    value: TheatreResponse;
    onDelete: (id: number) => void;
}

export default function TheatreAccordionItem(props: TheatreAccordionItemProps) {
    const [isOpen, setIsOpen] = useState(true);

    return (
        <div className="accordion-item">
            <h2 className="accordion-header">
                <button className={`accordion-button ${isOpen ? "" : "collapsed"}`} type="button" onClick={() => setIsOpen(!isOpen)} aria-expanded={isOpen} aria-controls={`panelsStayOpen-collapse${props.value.id}`}>
                    {props.value.name}
                </button>
            </h2>
        <div id={`panelsStayOpen-collapse${props.value.id}`} className={`accordion-collapse collapse ${isOpen ? "show" : ""}`}>
                <div className="accordion-body">
                    <div className="d-flex justify-content-between align-items-center">
                        <span>{props.value.address}</span>
                        <div className="d-flex justify-content-end mt-3">
                            <Link to={`/theatre/${props.value.id}`}>
                                <i className="bi bi-pencil-fill fs-5" />
                            </Link>
                            <Link to={""}>
                                <i className="ms-3 bi bi-trash-fill text-danger fs-5" onClick={() => props.onDelete(props.value.id)} />
                            </Link>
                        </div>
                    </div>
                    <MapField disable={true}
                        coordinates={{
                            longitude: props.value.longitude,
                            latitude: props.value.latitude,
                        }}
                        onUpdate={(data) => console.log(data)}
                    />
                </div>
            </div>
        </div>
    );
}
