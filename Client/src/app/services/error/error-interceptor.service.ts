import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ErrorInterceptorService implements HttpInterceptor{

  constructor() { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      retry(1),
      catchError((err) => {
        if (err.status === 401) {
          //refresh token or navigate to login.
          alert(err.error['errors'][0]);
        } 
        else if(err.status === 404) {
          //custom message.
          alert("404");
        }
        else if(err.status == 400) {
          //some other message
          alert(err.error['errors'][0]);
        }
        else {
          //global messagge.
          alert("Global Message");
        }
        return throwError(err);
      })
    )
  }
}
