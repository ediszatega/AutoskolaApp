import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
<<<<<<< Updated upstream
import { ApiService } from 'src/app/services/api.service';
=======
>>>>>>> Stashed changes
import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
<<<<<<< Updated upstream
export class DashboardComponent implements OnInit {
  public users: any = [];
  constructor(private auth: AuthService, private api: ApiService,private router: Router){}

  ngOnInit(): void {
    this.api.getUsers().subscribe(res=>{
      this.users=res;
    })
  }

  logout(){
=======
export class DashboardComponent {
  constructor(
    private auth: AuthService,
    private api: UserService,
    private router: Router
  ) {}

  logout() {
>>>>>>> Stashed changes
    this.auth.logout();
    this.router.navigate(['login']);
  }
}
