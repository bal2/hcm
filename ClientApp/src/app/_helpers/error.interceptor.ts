import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthenticationService } from '../login/authentication.service';
import { AlertService } from '../alert.service';



@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private authenticationService: AuthenticationService, private alertService: AlertService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError(err => {
            if (err.status === 401) {
                // auto logout if 401 response returned from api
                this.authenticationService.logout();
                location.reload(true);
            }
            else if (err.status === 403) {
                this.alertService.addGlobalAlert("danger", "You do not have permission to view this resource.");
            }
            else if (err.status === 400) {
                this.alertService.addLocalAlert("danger", err.error);
            }

            const error = err.error || err.statusText;
            return throwError(error);
        }))
    }
}