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
import { LecturersComponent } from './components/user/lecturers/lecturers.component';
import { LecturerFormComponent } from './components/user/lecturers/lecturer-form/lecturer-form.component';
import { AdminTestModuleComponent } from './components/admin-test-module/admin-test-module.component';
import { AddTestComponent } from './components/admin-test-module/add-test/add-test.component';
import { EditTestComponent } from './components/admin-test-module/edit-test/edit-test.component';
import { CategoryComponent } from './components/category/category.component';
import { AddCategoryComponent } from './components/category/add-category/add-category.component';
import { EditCategoryComponent } from './components/category/edit-category/edit-category.component';
import { VehicleComponent } from './components/vehicle/vehicle.component';
import { AddVehicleComponent } from './components/vehicle/add-vehicle/add-vehicle.component';
import { EditVehicleComponent } from './components/vehicle/edit-vehicle/edit-vehicle.component';
import { MottestComponent } from './components/mottest/mottest.component';
import { MottestFormComponent } from './components/mottest/mottest-form/mottest-form.component';
import { PaymentComponent } from './components/payment/payment.component';
import { PaymentFormComponent } from './components/payment/payment-form/payment-form.component';
import { environment } from 'src/environments/environment';
import { AngularFireModule } from '@angular/fire/compat';
import { AngularFireDatabaseModule } from '@angular/fire/compat/database';
import { AngularFireStorageModule } from '@angular/fire/compat/storage';
import { AdminNewsComponent } from './components/admin-news/admin-news.component';
import { AddNewsComponent } from './components/admin-news/add-news/add-news.component';
import { EditNewsComponent } from './components/admin-news/edit-news/edit-news.component';
import { NewsComponent } from './components/news/news.component';
import { NewsDetailsComponent } from './components/news/news-details/news-details.component';

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
    LecturersComponent,
    LecturerFormComponent,
    AdminTestModuleComponent,
    AddTestComponent,
    EditTestComponent,
    CategoryComponent,
    AddCategoryComponent,
    EditCategoryComponent,
    VehicleComponent,
    AddVehicleComponent,
    EditVehicleComponent,
    MottestComponent,
    MottestFormComponent,
    PaymentComponent,
    PaymentFormComponent,
    AdminNewsComponent,
    AddNewsComponent,
    EditNewsComponent,
    NewsComponent,
    NewsDetailsComponent,
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
    AngularFireModule.initializeApp(environment.firebaseConfig),
    AngularFireStorageModule,
    AngularFireDatabaseModule,
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
