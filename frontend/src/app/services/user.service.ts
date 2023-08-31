import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiConfig } from './api-config';
import { Observable, catchError, map, throwError } from 'rxjs';
import { User } from '../models/user';
import { convertDate } from './helper/utilities';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private baseUrl: string = ApiConfig.base_url;
  constructor(private http: HttpClient) {}

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl + '/User/GetAll');
  }
  getAdmins(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl + '/User/GetAdmins').pipe(
      map((admins) =>
        admins.map((admin) => ({
          ...admin,
          dateOfBirth: convertDate(admin.dateOfBirth.toString()),
        }))
      )
    );
  }
  getUsersIncludingCities(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl + '/User/GetAllIncludeCities');
  }

  removeUser(id: number): Observable<Object> {
    return this.http.delete(ApiConfig.base_url + `/User/Remove/${id}`);
  }

  addUser(user: any): any {
    return this.http.post(ApiConfig.base_url + '/User/Add', user);
  }

  addAdmin(admin: any): any {
    return this.http.post(ApiConfig.base_url + '/User/AddAdmin', admin);
  }

  updateUser(user: any): any {
    return this.http.put(ApiConfig.base_url + '/User/Update', user);
  }
}
