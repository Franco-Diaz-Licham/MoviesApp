/** Base model for selectors */
export interface BaseSelectorModel {
    id: number;
    selectionValue: () => string;
};