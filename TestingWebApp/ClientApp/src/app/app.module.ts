import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule }   from '@angular/forms';
import { AppComponent }   from './app.component';
import { HomeComponent } from './components/home/home.component'
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';

//Define routes
const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    { path: '**', redirectTo: '/' }
];

@NgModule({
    imports:      [ BrowserModule, FormsModule, HttpClientModule, RouterModule.forRoot(appRoutes) ],
    declarations: [ AppComponent, HomeComponent ],
    bootstrap:    [ AppComponent , HomeComponent ]
})

export class AppModule { }