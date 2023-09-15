import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiConfig } from './api-config';
import { Test } from '../models/test';
import { Observable } from 'rxjs';
import { Category } from '../models/category';
import { Question } from '../models/question';
import { Answer } from '../models/answer';

@Injectable({
  providedIn: 'root',
})
export class TestService {
  private baseUrl: string = ApiConfig.base_url;
  constructor(private http: HttpClient) {}

  getTestsByCategory(categoryId: number): Observable<Test[]> {
    const params = new HttpParams().set('categoryId', categoryId);
    return this.http.get<Test[]>(this.baseUrl + '/Test/GetAllByCategory', {
      params,
    });
  }

  getTests(): Observable<Test[]> {
    return this.http.get<Test[]>(this.baseUrl + '/Test/GetAllIncludeCategory');
  }

  getTestIncludeQuestionsAnswers(testId: number): Observable<Test> {
    return this.http.get<Test>(
      this.baseUrl + `/Test/GetByIdIncludeQuestionsAnswers/${testId}`
    );
  }

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(this.baseUrl + '/Category/GetAll');
  }

  getQuestionsByTest(testId: number): Observable<Question[]> {
    return this.http.get<Question[]>(
      this.baseUrl + '/Question/GetByTest/' + testId
    );
  }

  addTest(test: any): Observable<Test> {
    return this.http.post<Test>(this.baseUrl + '/Test/Add', test);
  }

  addAnswers(questionId: number, answers: Answer[]): Observable<Object> {
    return this.http.post<Answer[]>(
      this.baseUrl + '/Question/AddAnswers/' + questionId,
      answers
    );
  }

  addQuestion(question: Question): Observable<Question> {
    return this.http.post<Question>(this.baseUrl + '/Question/Add', question);
  }

  updateQuestion(question: Question): Observable<Object> {
    return this.http.put<Question>(this.baseUrl + '/Question/Update', question);
  }

  updateTest(test: Test): Observable<Object> {
    return this.http.put<Test>(this.baseUrl + '/Test/Update', test);
  }

  removeQuestion(question: Question): Observable<Object> {
    return this.http.delete<Question>(
      this.baseUrl + '/Question/Remove/' + question.id
    );
  }

  removeTest(test: Test): Observable<Object> {
    return this.http.delete<Test>(this.baseUrl + '/Test/Remove/' + test.id);
  }
}
