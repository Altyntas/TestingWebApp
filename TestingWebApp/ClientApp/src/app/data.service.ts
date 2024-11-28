import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { FileVM } from './models/FileModel';
import { retry } from 'rxjs';

@Injectable()

export class DataService {
    private url = "/api/home";

    constructor(private http: HttpClient) { }

    uploadFile(file: FileVM) {
        try {
            return this.http.post(this.url + "/UploadFile", file).pipe().subscribe();
        }
        catch (e) {
            console.log(e);
        }
    }
}