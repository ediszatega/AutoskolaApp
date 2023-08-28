import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiConfig } from './api-config';
import { Test } from '../models/test';
import { Observable } from 'rxjs';
import { Category } from '../models/category';

@Injectable({
  providedIn: 'root',
})
export class TestService {
  private baseUrl: string = ApiConfig.base_url;
  constructor(private http: HttpClient) {}

  getTests(
    pageNumber: number,
    pageSize: number,
    categoryId: number
  ): Observable<Test[]> {
    const params = new HttpParams().set('categoryId', categoryId);
    return this.http.get<Test[]>(this.baseUrl + '/Test/GetAllByCategory', {
      params,
    });
  }

  getTestIncludeQuestionsAnswers(testId: number): Observable<Test> {
    return this.http.get<Test>(
      this.baseUrl + `/Test/GetByIdIncludeQuestionsAnswers/${testId}`
    );
  }

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(this.baseUrl + '/Category/GetAll');
  }
}
