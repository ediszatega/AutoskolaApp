import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { CitiesComponent } from './components/cities/cities.component';
import { FormsModule } from '@angular/forms';
import { RouterModule, RouterOutlet } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [AppComponent, CitiesComponent],
  imports: [
    BrowserModule,
    FormsModule,
    RouterOutlet,
    HttpClientModule,
    RouterModule.forRoot([{ path: 'path-cities', component: CitiesComponent }]),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
