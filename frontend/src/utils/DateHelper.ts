
/** Helper function to process datetime. */
export default function getDate(date: Date) {
    return date.toISOString().split("T")[0];
}
