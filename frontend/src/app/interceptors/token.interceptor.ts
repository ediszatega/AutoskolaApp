import { Injectable, IterableDiffers } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(private auth: AuthService, private router: Router, private toast: NgToastService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const token = this.auth.getToken();
    if(token){
      request=request.clone({
        setHeaders: {Authorization: `Bearer ${token}`}
      })
    }
    return next.handle(request).pipe(
      catchError((err:any)=>{
        if(err instanceof HttpErrorResponse){
          if(err.status === 401) {
            this.toast.warning({detail:"Warning", summary:"Session has expired, please log in again", duration: 5000});
            this.router.navigate(['login']);
          }
        }
        return throwError(()=> new Error("Some other error occured"));
      })
    );
  }
}
