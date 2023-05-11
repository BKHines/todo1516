import { Signal } from "@angular/core";

export interface TodoItem {
    id?: number;
    description: string;
    order: number;
    status: string;
    itemtype: string;
    updated?: string;
    userident?: string;

    // Front end properties
    editable?: any; // Signal<boolean>;
}