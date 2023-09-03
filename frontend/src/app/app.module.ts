import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, RouterOutlet } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { UserComponent } from './components/user/user.component';
import { CitiesComponent } from './components/cities/cities.component';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { AppRoutingModule } from './app-routing.module';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { NgToastModule } from 'ng-angular-popup';
import { TokenInterceptor } from './interceptors/token.interceptor';
import { TestModuleComponent } from './components/test-module/test-module.component';
import { TestCategoryComponent } from './components/test-module/test-category/test-category.component';
import { TestSelectionComponent } from './components/test-module/test-selection/test-selection.component';
import { PageTitleComponent } from './components/common/page-title/page-title.component';
import { TestWorkingComponent } from './components/test-module/test-working/test-working.component';
import { AdminsComponent } from './components/user/admins/admins.component';
import { PopupComponent } from './components/common/popup/popup.component';
import { AdminFormComponent } from './components/user/admins/admin-form/admin-form.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { CustomersComponent } from './components/user/customers/customers.component';
import { CustomerFormComponent } from './components/user/customers/customer-form/customer-form.component';
import { InstructorsComponent } from './components/user/instructors/instructors.component';
import { InstructorFormComponent } from './components/user/instructors/instructor-form/instructor-form.component';

@NgModule({
  declarations: [
    AppComponent,
    UserComponent,
    CitiesComponent,
    LoginComponent,
    SignupComponent,
    DashboardComponent,
    TestModuleComponent,
    TestCategoryComponent,
    TestSelectionComponent,
    PageTitleComponent,
    TestWorkingComponent,
    AdminsComponent,
    PopupComponent,
    AdminFormComponent,
    CustomersComponent,
    CustomerFormComponent,
    InstructorsComponent,
    InstructorFormComponent,
  ],
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
    NgToastModule,
    NgxPaginationModule,
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
