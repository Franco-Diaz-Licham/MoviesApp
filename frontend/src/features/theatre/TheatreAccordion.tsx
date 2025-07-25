import { TheatreResponse } from "../../types/theatre/TheatreResponse.type"
import TheatreAccordionItem from "./TheatreAccordionItem";

/** Function props. */
interface TheatreAccordionProps{
    values: TheatreResponse[];
    onDelete: (id: number) => void;
}

/** Display map and theatre location information. */
export default function TheatreAccordion(props: TheatreAccordionProps){
    return(
        <div className="accordion" id="accordionPanelsStayOpenExample">
            {props.values.map((data: TheatreResponse) =>{
                return <TheatreAccordionItem key={data.id} value={data} onDelete={props.onDelete}/>
            })}
        </div>
    )
}