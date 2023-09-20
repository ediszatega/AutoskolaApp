import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { AuthService } from 'src/app/services/auth.service';
import { UserComponent } from '../user/user.component';
import { AdminTestModuleComponent } from '../admin-test-module/admin-test-module.component';
import { CategoryComponent } from '../category/category.component';
import { VehicleComponent } from '../vehicle/vehicle.component';
import { MottestComponent } from '../mottest/mottest.component';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
  public links = [
    { label: 'Korisnici', component: UserComponent, class: '' },
    { label: 'Testovi', component: AdminTestModuleComponent, class: '' },
    { label: 'Kategorije', component: CategoryComponent, class: '' },
    { label: 'Vozila', component: VehicleComponent, class: '' },
    { label: 'Servisi', component: MottestComponent, class: '' },
  ];
  public activeLink: number;
  public currentComponent;
  constructor(private auth: AuthService, private router: Router) {}

  ngOnInit(): void {
    this.activeLink = 0;
    this.currentComponent = this.links[this.activeLink].component;
    this.links[this.activeLink].class = 'active';
  }

  onLinkClick(linkIndex: number) {
    this.links[this.activeLink].class = '';
    this.activeLink = linkIndex;
    this.currentComponent = this.links[this.activeLink].component;
    this.links[this.activeLink].class = 'active';
  }

  logout() {
    this.auth.logout();
    this.router.navigate(['login']);
  }
}
