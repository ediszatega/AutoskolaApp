import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiConfig } from './api-config';
import { Observable } from 'rxjs';
import { News } from '../models/news';

@Injectable({
  providedIn: 'root',
})
export class NewsService {
  private baseUrl: string = ApiConfig.base_url;

  constructor(private http: HttpClient) {}

  getNews(): Observable<News[]> {
    return this.http.get<News[]>(this.baseUrl + '/News/GetAll');
  }

  addNews(news: any): Observable<Object> {
    return this.http.post(this.baseUrl + '/News/Add', news);
  }

  updateNews(news: News): Observable<Object> {
    return this.http.put(this.baseUrl + '/News/Update', news);
  }

  deleteNews(news: News): Observable<Object> {
    return this.http.delete(this.baseUrl + '/News/Remove/' + news.id);
  }
}
