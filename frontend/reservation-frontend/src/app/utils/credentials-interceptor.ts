import { HttpEvent, HttpHandlerFn, HttpRequest } from "@angular/common/http";
import { Observable } from "rxjs";


export function credentialsInterceptor (
    req: HttpRequest<unknown>, 
    next: HttpHandlerFn
): Observable<HttpEvent<unknown>> {
    const updatedReq = req.clone(
        {withCredentials: true}
    )
    return next(updatedReq);
}