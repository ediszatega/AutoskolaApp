import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { City } from '../models/city';
import { ApiConfig } from './api-config';

@Injectable({
  providedIn: 'root',
})
export class CityService {
  private baseUrl: string = ApiConfig.base_url;
  constructor(private http: HttpClient) {}

  getCities(): Observable<City[]> {
    return this.http.get<City[]>(this.baseUrl + '/City/GetAll');
  }
}
