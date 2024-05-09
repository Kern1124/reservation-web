import { HttpContext, HttpErrorResponse, HttpHeaders, HttpParams } from "@angular/common/http";

export interface Options {
    headers?: HttpHeaders | {
        [header: string]: string | string[];
    };
    context?: HttpContext;
    observe?: 'body';
    params?: HttpParams | {
        [param: string]: string | number | boolean | ReadonlyArray<string | number | boolean>;
    };
    reportProgress?: boolean;
    responseType: 'json';
    withCredentials?: boolean;
}

export interface UserDto {
    username: string,
    mailAddress: string
}

export interface LoginRequest {
    mailAddress: string,
    password: string
}
export interface LoginResponse {
    user: UserDto
}
export interface AuthenticatedResponse{
    error: HttpErrorResponse
    user?: UserDto
}
export interface ProblemDetails {
    type: string,
    title: string,
    status: number,
    instance: string,
    traceId: string,
    errors: {
        name: string,
        reason: string
    }[]
}

export interface RegisterRequest {
    mailAddress: string,
    username: string,
    password: string
}
export interface RegisterResponse {
    message: string
}