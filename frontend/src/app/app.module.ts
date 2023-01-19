import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, RouterOutlet } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { UserComponent } from './components/user/user.component';
import { CitiesComponent } from './components/cities/cities.component';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { AppRoutingModule } from './app-routing.module';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { NgToastModule } from 'ng-angular-popup';

@NgModule({
  declarations: [AppComponent, UserComponent, CitiesComponent, LoginComponent, SignupComponent, DashboardComponent],
  imports: [
    BrowserModule,
    RouterModule.forRoot([
      { path: 'user', component: UserComponent },
      { path: 'path-cities', component: CitiesComponent },
    ]),
    HttpClientModule,
    FormsModule,
    RouterOutlet,
    AppRoutingModule,
    ReactiveFormsModule,
    NgToastModule
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
