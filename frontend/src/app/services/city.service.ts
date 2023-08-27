import { Injectable } from '@angular/core';
import { ApiConfig } from './api-config';
import { HttpClient } from '@angular/common/http';
import { City } from '../models/city';
import { Observable } from 'rxjs';

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
