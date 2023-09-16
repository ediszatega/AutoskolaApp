import { Injectable } from '@angular/core';
import { ApiConfig } from './api-config';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Category } from '../models/category';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  private baseUrl: string = ApiConfig.base_url;

  constructor(private http: HttpClient) {}

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(this.baseUrl + '/Category/GetAll');
  }

  addCategory(category: any): Observable<Category> {
    return this.http.post<Category>(this.baseUrl + '/Category/Add', category);
  }

  updateCategory(category: any): Observable<Category> {
    return this.http.put<Category>(this.baseUrl + '/Category/Update', category);
  }

  removeCategory(id: number): Observable<Object> {
    return this.http.delete(this.baseUrl + `/Category/Remove/${id}`);
  }
}
