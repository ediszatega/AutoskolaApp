import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiConfig } from './api-config';
import { Observable, map } from 'rxjs';
import { Payment } from '../models/payment';
import { convertDate } from './helper/utilities';

@Injectable({
  providedIn: 'root',
})
export class PaymentService {
  baseUrl: string = ApiConfig.base_url;

  constructor(private http: HttpClient) {}

  getPayments(): Observable<Payment[]> {
    return this.http.get<Payment[]>(this.baseUrl + '/Payment/GetAll').pipe(
      map((payments) =>
        payments.map((payment) => ({
          ...payment,
          date: convertDate(payment.date.toString()),
        }))
      )
    );
  }

  addPayment(payment: any): Observable<Object> {
    return this.http.post(this.baseUrl + '/Payment/Add', payment);
  }

  updatePayment(payment: any): Observable<Object> {
    return this.http.put(this.baseUrl + '/Payment/Update', payment);
  }

  removePayment(id: number): Observable<Object> {
    return this.http.delete(this.baseUrl + `/Payment/Remove/${id}`);
  }
}
