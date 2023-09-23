import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiConfig } from './api-config';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private baseUrl: string = ApiConfig.base_url + '/User';
  constructor(private http: HttpClient) {}

  login(userLogin: any) {
    return this.http.post<any>(`${this.baseUrl}/Authenticate`, userLogin);
  }

  register(userRegister: any) {
    return this.http.post<any>(`${this.baseUrl}/Register`, userRegister);
  }

  storeToken(tokenValue: string) {
    localStorage.setItem('token', tokenValue);
  }

  storeUser(user: Object) {
    localStorage.setItem('user', JSON.stringify(user));
  }

  getUser() {
    const user = localStorage.getItem('user');
    return JSON.parse(user);
  }

  getRole() {
    let user = localStorage.getItem('user');
    return JSON.parse(user).role;
  }

  getToken() {
    return localStorage.getItem('token');
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }

  logout() {
    localStorage.clear();
  }
}
