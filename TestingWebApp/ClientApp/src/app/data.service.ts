import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { FileVM } from './models/FileModel';

@Injectable()

export class DataService {
    private url = "/api/home";

    constructor(private http: HttpClient) { }

    uploadFile(file: FormData) {
        try {
            return this.http.post(this.url + "/UploadFile", file);
        }
        catch (e) {
            console.log(e);
        }
    }
}