import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { FileVM } from './models/FileModel';
import { DatasetsVM } from './models/DatasetsModel';

@Injectable()

export class DataService {
    constructor(private http: HttpClient) { }

    uploadFile(file: FormData) {
        try {
            return this.http.post("/api/home/UploadFile", file);
        }
        catch (e) {
            console.log(e);
        }
    }
    
    getAllDatasets() {
        try {
            return this.http.get<DatasetsVM[]>("/api/datasets/GetAllDatasets");
        }
        catch (e) {
            console.log(e);
        }
    }
}