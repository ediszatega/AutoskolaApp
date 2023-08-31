import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { AuthService } from 'src/app/services/auth.service';
import { UserComponent } from '../user/user.component';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
  public links = [{ label: 'Korisnici', component: UserComponent, class: '' }];
  public currentComponent;
  constructor(private auth: AuthService, private router: Router) {}

  ngOnInit(): void {
    this.currentComponent = this.links[0].component;
    this.links[0].class = 'active';
  }

  logout() {
    this.auth.logout();
    this.router.navigate(['login']);
  }
}
