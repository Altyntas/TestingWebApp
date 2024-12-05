import { Component, OnInit } from '@angular/core';
import { DataService } from '../../data.service';
import { DatasetsVM } from '../../models/DatasetsModel';
 
@Component({
    templateUrl: './datasets.component.html',
    providers: [DataService]
})

export class DatasetsComponent implements OnInit {
    datasetsVM: DatasetsVM[];
    constructor(private dataService: DataService) { }

    ngOnInit(): void {
        this.loadDatasets();
    }

    loadDatasets() {
        this.dataService.getAllDatasets()?.subscribe((data: DatasetsVM[]) => this.datasetsVM = data);
    }
}