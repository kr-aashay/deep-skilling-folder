import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    // Clone request and add Authorization header to every outgoing HTTP request
    const authReq = req.clone({
      setHeaders: { Authorization: 'Bearer mocktoken-12345' }
    });
    return next.handle(authReq);
  }
}
