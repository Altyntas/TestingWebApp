import { ColumnsVM } from "./ColumnsModel";

export class DatasetsVM {
    constructor(public id?: string,
        public name?: string,
        public columns?: ColumnsVM[]) {}
}