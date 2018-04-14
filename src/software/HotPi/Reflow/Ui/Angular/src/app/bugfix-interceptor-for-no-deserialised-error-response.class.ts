import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HttpErrorResponse
} from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/throw';

@Injectable()
export class BugfixInterceptorForNoDeserialisedErrorResponse implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).catch(event => {
      if (event instanceof HttpErrorResponse) {
        const response = event as HttpErrorResponse;
        if (response.headers.get('content-type') === 'application/json') {
          return Observable.throw(new HttpErrorResponse({
            error: JSON.parse(response.error),
            headers: response.headers,
            status: response.status,
            statusText: response.statusText,
            url: response.url,
          }));
        }
      }

      return Observable.throw(event);
    })
  }
}
