import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, RouterOutlet } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { CitiesComponent } from './components/cities/cities.component';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { AppRoutingModule } from './app-routing.module';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { NgToastModule } from 'ng-angular-popup';
import { TokenInterceptor } from './interceptors/token.interceptor';
<<<<<<< Updated upstream

@NgModule({
  declarations: [AppComponent, UserComponent, CitiesComponent, LoginComponent, SignupComponent, DashboardComponent],
=======
import { TestModuleComponent } from './components/test-module/test-module.component';
import { TestCategoryComponent } from './components/test-module/test-category/test-category.component';
import { TestSelectionComponent } from './components/test-module/test-selection/test-selection.component';
import { PageTitleComponent } from './components/common/page-title/page-title.component';
import { TestWorkingComponent } from './components/test-module/test-working/test-working.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';

@NgModule({
  declarations: [
    AppComponent,
    CitiesComponent,
    LoginComponent,
    SignupComponent,
    DashboardComponent,
    TestModuleComponent,
    TestCategoryComponent,
    TestSelectionComponent,
    PageTitleComponent,
    TestWorkingComponent,
  ],
>>>>>>> Stashed changes
  imports: [
    BrowserModule,
    RouterModule.forRoot([{ path: 'path-cities', component: CitiesComponent }]),
    HttpClientModule,
    FormsModule,
    RouterOutlet,
    AppRoutingModule,
    ReactiveFormsModule,
    NgToastModule,
    BrowserAnimationsModule,
    MatSidenavModule,
    MatListModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
