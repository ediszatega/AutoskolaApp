import { Injectable } from '@angular/core';
import { ApiConfig } from './api-config';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Vehicle } from '../models/vehicle';

@Injectable({
  providedIn: 'root',
})
export class VehicleService {
  private baseUrl: string = ApiConfig.base_url;

  constructor(private http: HttpClient) {}

  getVehicles(): Observable<Vehicle[]> {
    return this.http.get<Vehicle[]>(this.baseUrl + '/Vehicle/GetAll');
  }

  addVehicle(vehicle: any): Observable<Vehicle> {
    return this.http.post<Vehicle>(this.baseUrl + '/Vehicle/Add', vehicle);
  }

  updateVehicle(vehicle: any): Observable<Vehicle> {
    return this.http.put<Vehicle>(this.baseUrl + '/Vehicle/Update', vehicle);
  }

  removeVehicle(id: number): Observable<Vehicle> {
    return this.http.delete<Vehicle>(this.baseUrl + `/Vehicle/Remove/${id}`);
  }
}
