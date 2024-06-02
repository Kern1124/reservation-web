import { HttpErrorResponse, HttpEvent, HttpHandlerFn, HttpRequest } from "@angular/common/http";
import { inject } from "@angular/core";
import { catchError, Observable, throwError } from "rxjs";
import { AuthService } from "../services/auth.service";
import { Router } from "@angular/router";


export function credentialsInterceptor (
    req: HttpRequest<unknown>, 
    next: HttpHandlerFn,
): Observable<HttpEvent<unknown>> {
    const updatedReq = req.clone(
        {withCredentials: true}
    )
    return next(updatedReq).pipe(catchError(
        (err: any) => {
            if (err instanceof HttpErrorResponse){
                if (err.status === 401 || err.status === 404) {
                    // empty for now
                }
            }
            return throwError(() => err); 
        }
    ))
}