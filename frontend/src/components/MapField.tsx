import { MapContainer, Marker, TileLayer, useMap, useMapEvent } from "react-leaflet";
import L, { LeafletMouseEvent, Popup } from "leaflet";
import icon from "leaflet/dist/images/marker-icon.png";
import iconShadow from "leaflet/dist/images/marker-shadow.png";
import "leaflet/dist/leaflet.css";
import CoordinateDTO from "../types/theatre/CoordinateDTO.type";
import { useState } from "react";

/** Function props.*/
interface MapFieldProps {
    coordinates: CoordinateDTO;
    disable: boolean;
    /** Callback for form handling. */
    onUpdate(coordinates: CoordinateDTO): void;
}

/** Function props. */
interface MapClickProps {
    setCoordinates(coordinates: CoordinateDTO): void;
}

/** Component to handle change in coordinates based on user click. */
function MapClick(props: MapClickProps) {
    useMapEvent("click", (eventArgs: LeafletMouseEvent) => {
        props.setCoordinates({
            latitude: eventArgs.latlng.lat,
            longitude: eventArgs.latlng.lng,
        });
    });
    return null;
}

/** Leaflet field for map locations. */
export default function MapField(props: MapFieldProps) {
    let defaultIcon = L.icon({
        iconUrl: icon,
        shadowUrl: iconShadow,
        iconAnchor: [16, 37],
    });

    L.Marker.prototype.options.icon = defaultIcon;

    const [coord, setCoord] = useState<CoordinateDTO>(props.coordinates);

    const addClick = () =>
        props.disable ? (
            <></>
        ) : (
            <MapClick
                setCoordinates={(coord: CoordinateDTO) => {
                    setCoord(coord);
                    props.onUpdate(coord);
                }}
            />
        );

    return (
        <div style={{ height: "400px" }} className="mt-4">
            <MapContainer attributionControl={false} dragging={!props.disable} zoomControl={!props.disable} scrollWheelZoom={!props.disable} center={[props.coordinates.latitude, props.coordinates.longitude]} zoom={14} style={{ height: "100%", width: "100%" }}>
                <TileLayer url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />
                {addClick()}
                {coord ? <Marker position={[coord.latitude, coord.longitude]} /> : null}
            </MapContainer>
        </div>
    );
}
