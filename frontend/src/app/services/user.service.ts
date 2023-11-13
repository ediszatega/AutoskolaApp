import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiConfig } from './api-config';
import {
  Observable,
  ObservedValuesFromArray,
  catchError,
  map,
  throwError,
} from 'rxjs';
import { User } from '../models/user';
import { convertDate } from './helper/utilities';
import { Customer } from '../models/customer';
import { Instructor } from '../models/instructor';
import { Lecturer } from '../models/lecturer';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private baseUrl: string = ApiConfig.base_url;
  constructor(private http: HttpClient) {}

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl + '/User/GetAll');
  }
  getUsersIncludingCities(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl + '/User/GetAllIncludeCities');
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

  getCustomers(): Observable<Customer[]> {
    return this.http
      .get<Customer[]>(this.baseUrl + '/Customer/GetAllIncludeCities')
      .pipe(
        map((customers) =>
          customers.map((customer) => ({
            ...customer,
            dateOfBirth: convertDate(customer.dateOfBirth.toString()),
          }))
        )
      );
  }

  getInstructors(): Observable<Instructor[]> {
    return this.http
      .get<Instructor[]>(this.baseUrl + '/Instructor/GetAllIncludeCities')
      .pipe(
        map((instructors) =>
          instructors.map((instructor) => ({
            ...instructor,
            dateOfBirth: convertDate(instructor.dateOfBirth.toString()),
          }))
        )
      );
  }

  getLecturers(): Observable<Lecturer[]> {
    return this.http
      .get<Lecturer[]>(this.baseUrl + '/Lecturer/GetAllIncludeCities')
      .pipe(
        map((lecturers) =>
          lecturers.map((lecturer) => ({
            ...lecturer,
            dateOfBirth: convertDate(lecturer.dateOfBirth.toString()),
          }))
        )
      );
  }

  getEmployeesWithScore(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + '/Employee/GetAllWithScore');
  }

  removeUser(id: number): Observable<Object> {
    return this.http.delete(ApiConfig.base_url + `/User/Deactivate/${id}`);
  }
  addUser(user: any): Observable<Object> {
    return this.http.post(ApiConfig.base_url + '/User/Add', user);
  }

  addAdmin(admin: any): Observable<Object> {
    return this.http.post(ApiConfig.base_url + '/User/AddAdmin', admin);
  }

  addCustomer(user: any): Observable<Object> {
    return this.http.post(ApiConfig.base_url + '/Customer/Add', user);
  }

  addInstructor(user: any): Observable<Object> {
    return this.http.post(ApiConfig.base_url + '/Instructor/Add', user);
  }

  addLecturer(user: any): Observable<Object> {
    return this.http.post(ApiConfig.base_url + '/Lecturer/Add', user);
  }

  updateUser(user: any): Observable<Object> {
    return this.http.put(ApiConfig.base_url + '/User/Update', user);
  }

  updateCustomer(user: any): Observable<Object> {
    return this.http.put(ApiConfig.base_url + '/Customer/Update', user);
  }

  updateInstructor(user: any): Observable<Object> {
    return this.http.put(ApiConfig.base_url + '/Instructor/Update', user);
  }

  updateLecturer(user: any): Observable<Object> {
    return this.http.put(ApiConfig.base_url + '/Lecturer/Update', user);
  }
}
