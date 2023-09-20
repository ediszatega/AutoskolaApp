import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiConfig } from './api-config';
import { Observable, map } from 'rxjs';
import { MotTest } from '../models/mottest';
import { convertDate } from './helper/utilities';

@Injectable({
  providedIn: 'root',
})
export class MottestService {
  baseUrl: string = ApiConfig.base_url;

  constructor(private http: HttpClient) {}

  getMotTests(): Observable<MotTest[]> {
    return this.http.get<MotTest[]>(this.baseUrl + '/MotTest/GetAll').pipe(
      map((mottests) =>
        mottests.map((mottest) => ({
          ...mottest,
          date: convertDate(mottest.date.toString()),
        }))
      )
    );
  }

  addMotTest(mottest: any): Observable<MotTest> {
    return this.http.post<MotTest>(this.baseUrl + '/MotTest/Add', mottest);
  }

  updateMotTest(mottest: any): Observable<MotTest> {
    return this.http.put<MotTest>(this.baseUrl + '/MotTest/Update', mottest);
  }

  removeMotTest(id: number): Observable<Object> {
    return this.http.delete(this.baseUrl + `/MotTest/Remove/${id}`);
  }
}
