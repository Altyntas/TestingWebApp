import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule }   from '@angular/forms';
import { AppComponent }   from './app.component';
import { HomeComponent } from './components/home/home.component'
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';
import { DatasetsComponent } from './components/datasets/datasets.component';
import { DatasetInfoComponent } from './components/dataset-info/dataset-info.component';

//Define routes
const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'datasets', component: DatasetsComponent },
    { path: 'dataset-info', component: DatasetInfoComponent},
    { path: '**', redirectTo: '/' }
];

@NgModule({
    imports:      [ BrowserModule, FormsModule, HttpClientModule, RouterModule.forRoot(appRoutes) ],
    declarations: [ AppComponent, HomeComponent, DatasetsComponent, DatasetInfoComponent ],
    bootstrap:    [ AppComponent , HomeComponent, DatasetsComponent, DatasetInfoComponent ]
})

export class AppModule { }