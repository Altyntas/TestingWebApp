export class FileVM {
    constructor(
        public id?: number,
        public name?: string,
        public description?: string,
        public fullPath?: string,
        public file?: File) { }
}