import { Component, OnInit } from '@angular/core';
import { DataService } from '../../data.service';
import { FileVM } from '../../models/FileModel';
 
@Component({
    templateUrl: './home.component.html',
    providers: [DataService]
})

export class HomeComponent implements OnInit {
    file: FileVM = new FileVM();

    constructor(private dataService: DataService) { }

    ngOnInit(): void {
        
    }

    uploadFile(){
        try {
            var file = new FileVM(1, "FileTestName", "TestDescr", "It works!");
            var test = this.dataService.uploadFile(file);
        }
        catch (e) {
            console.log(e);
        }
    }
}