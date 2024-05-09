import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { AuthenticatedResponse, LoginRequest, LoginResponse, Options, RegisterRequest, RegisterResponse, UserDto } from 'src/types';
import { catchError, map, Observable, of, tap } from 'rxjs';
import { env } from '../env';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  static instance: AuthService;
  isAuthenticated$: Observable<boolean>;
  constructor(private api: ApiService) {
    if (!this.isAuthenticated$){
      this.isAuthenticated$ = this.isAuthenticated()
    }
  }

  login = (body: LoginRequest): Observable<LoginResponse> => {
    return this.api.post(`${env.apiUrl}/users/login`, body)
  }

  logout = (): void => {
    this.api.post(`${env.apiUrl}/users/logout`).subscribe({
      next: () => this.refreshAuthenticated()
    });
  }

  register = (body: RegisterRequest): Observable<RegisterResponse> => {
    return this.api.post(`${env.apiUrl}/users/register`, body)
  }

  getAuthenticatedUser = (): Observable<AuthenticatedResponse> => {
    return this.api.get<AuthenticatedResponse>(`${env.apiUrl}/users/authenticated`, {
        responseType: "json"
      })
  }

  refreshAuthenticated = () => {
    this.isAuthenticated$ = this.isAuthenticated()
  }

  isAuthenticated = (): Observable<boolean> => {
    return this.getAuthenticatedUser().pipe(
      map((response) => {
          return true;
      }),
      catchError( (error) => {return of(false)}));
  }
}
