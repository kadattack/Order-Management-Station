import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import {AccountService} from "../_services/account.service";
import {take} from "rxjs/operators";
import {Oms} from "../interfaces/oms";

// Add {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true} to app.module.ts


@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private accountService : AccountService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    let currentOmsUser: Oms | undefined;

    this.accountService.currentUser$.pipe(take(1)).subscribe(savedOms => currentOmsUser = savedOms);
    if (currentOmsUser){
      request= request.clone({
        setHeaders:{
          clientToken: `${currentOmsUser.clientToken}`
        }
      })

    }
    return next.handle(request);
  }
}
