import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { AuthGuard } from './guards/auth.guard';
<<<<<<< Updated upstream

const routes: Routes=[
  {path:'login', component:LoginComponent},
  {path:'signup', component:SignupComponent},
  {path:'dashboard', component:DashboardComponent, canActivate:[AuthGuard]}
=======
import { TestModuleComponent } from './components/test-module/test-module.component';
import { TestWorkingComponent } from './components/test-module/test-working/test-working.component';
import { UserComponent } from './components/dashboard/user/user.component';
import { CitiesComponent } from './components/cities/cities.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'signup', component: SignupComponent },
  {
    path: 'dashboard',
    component: DashboardComponent,
  },
  {
    path: 'dashboard/users',
    component: UserComponent,
  },
  {
    path: 'dashboard/cities',
    component: CitiesComponent,
  },
  {
    path: 'dashboard/tests',
    component: CitiesComponent,
  },
  { path: 'tests', component: TestModuleComponent },
  { path: 'tests/:id', component: TestWorkingComponent },
>>>>>>> Stashed changes
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
