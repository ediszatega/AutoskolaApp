import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiConfig } from './api-config';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl: string = ApiConfig.base_url;
  constructor(private http: HttpClient) { }

  getUsers() {
    return this.http.get<any>(this.baseUrl+"/User/GetAll");
  }
}
