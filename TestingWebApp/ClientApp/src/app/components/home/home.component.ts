import { Component, OnInit } from '@angular/core';
import { DataService } from '../../data.service';
import { FileVM } from '../../models/FileModel';
import { Observable } from 'rxjs';
 
@Component({
    templateUrl: './home.component.html',
    providers: [DataService]
})

export class HomeComponent implements OnInit {
    fileVM: FileVM = new FileVM();

    constructor(private dataService: DataService) { }

    ngOnInit(): void {
        
    }

    uploadFile(fileVM: FormData){
        try {
            this.dataService.uploadFile(fileVM).pipe().subscribe((s) => alert(s));
        }
        catch (e) {
            console.log(e);
        }
    }

    addFile() {
        document.querySelector('input').click();
    }

    handle(e) {
        var file = e.target.files[0];
        var testFile = new FormData();
        testFile.append("id", "0");
        testFile.append("name", file.name);
        testFile.append("description", "Place for description coming soon");
        testFile.append("fullPath", "Coming Soon");
        testFile.append("file", file)
        this.uploadFile(testFile);
    }
}