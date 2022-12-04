import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiConfig } from '../../../services/api-config';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
})
export class UserComponent implements OnInit {
  allUsers: any;
  search: string = '';

  constructor(private httpClient: HttpClient) {}

  ngOnInit(): void {
    this.fetchData();
  }

  private fetchData() {
    this.httpClient
      .get(ApiConfig.base_url + '/User/GetAll')
      .subscribe((x: any) => {
        this.allUsers = x;
      });
  }

  getUsers() {
    if (this.allUsers == null) return [];
    return this.allUsers;
  }
}
