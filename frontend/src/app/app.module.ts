import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, RouterOutlet } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { UserComponent } from './components/user/user.component';
import { CitiesComponent } from './components/cities/cities.component';

@NgModule({
  declarations: [AppComponent, UserComponent, CitiesComponent],
  imports: [
    BrowserModule,
    RouterModule.forRoot([
      { path: 'user', component: UserComponent },
      { path: 'path-cities', component: CitiesComponent },
    ]),
    HttpClientModule,
    FormsModule,
    RouterOutlet,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
