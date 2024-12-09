import { Component, OnInit } from '@angular/core';
import { DataService } from '../../data.service';
import { DatasetsVM } from '../../models/DatasetsModel';
import { ChartService } from '../../common/chart/chart.service';
import { Chart } from 'chart.js';
 
@Component({
    templateUrl: './dataset-info.component.html',
    providers: [DataService, ChartService]
})

export class DatasetInfoComponent implements OnInit {
    datasetVM: DatasetsVM;
    public chart: any;

    constructor(private dataService: DataService, private chartService: ChartService) { }

    ngOnInit(): void {
        this.chart = this.chartService.createChart();
    }

}