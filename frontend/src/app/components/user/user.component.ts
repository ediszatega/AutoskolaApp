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
  selectedUser: any;
  allCities: any;
  cityId: number = 0;

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

    this.httpClient.get(ApiConfig.base_url + '/City/GetAll').subscribe((x) => {
      this.allCities = x;
    });
  }

  getUsers() {
    if (this.allUsers == null) return [];
    return this.allUsers.filter((x: any) =>
      x.firstName.toLowerCase().startsWith(this.search.toLowerCase())
    );
  }

  removeUser(id: number) {
    this.httpClient
      .delete(ApiConfig.base_url + `/User/Remove/${id}`)
      .subscribe((x: any) => {
        this.fetchData();
      });
  }

  addUser() {
    this.selectedUser = {
      id: 0,
      firstName: '',
      lastName: '',
      username: '',
      password: '',
      cityId: this.cityId,
    };
  }

  save() {
    this.selectedUser.cityId = this.cityId;
    console.log(this.selectedUser);
    console.log(this.cityId);
    if (this.selectedUser.id == 0) {
      this.httpClient
        .post(ApiConfig.base_url + '/User/Add', this.selectedUser)
        .subscribe((x: any) => {
          this.fetchData();
          this.addUser();
        });
    }
  }

  cancel() {
    this.selectedUser = null;
    this.fetchData();
  }
}
