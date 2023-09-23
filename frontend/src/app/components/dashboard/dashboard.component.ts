import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { AuthService } from 'src/app/services/auth.service';
import { UserComponent } from '../user/user.component';
import { AdminTestModuleComponent } from '../admin-test-module/admin-test-module.component';
import { AdminNewsComponent } from '../admin-news/admin-news.component';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
  public links = [
    { label: 'Korisnici', component: UserComponent, class: '' },
    { label: 'Testovi', component: AdminTestModuleComponent, class: '' },
    { label: 'Novosti', component: AdminNewsComponent, class: '' },
  ];
  public activeLink: number;
  public currentComponent;
  constructor(private auth: AuthService, private router: Router) {}

  ngOnInit(): void {
    this.activeLink = 1;
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
