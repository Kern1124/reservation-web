import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { AuthenticatedResponse, LoginRequest, LoginResponse, NotificationDto, Options, RegisterRequest, RegisterResponse, UserDto } from 'src/types';
import { BehaviorSubject, catchError, map, Observable, of, shareReplay, Subject, tap } from 'rxjs';
import { env } from '../env';
import { NotificationsService } from './notifications.service';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  authenticatedUser: Subject<UserDto | undefined> = new Subject();
  isAuthenticated$: Subject<boolean> = new Subject();
  constructor(private api: ApiService) {
    this.refreshAuthenticated()
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

  isAuthenticatedFn = (): boolean => {
    return localStorage.getItem("authenticated") !== null;
  }

  getAuthenticatedUser = (): Observable<AuthenticatedResponse> => {
    return this.api.get<AuthenticatedResponse>(`${env.apiUrl}/users/authenticated`, {
        responseType: "json"
      })
  }

  refreshAuthenticated = () => {
    this.getAuthenticatedUser().subscribe(
    {
      next: v => {
        localStorage.setItem("authenticated","true")
        this.authenticatedUser.next(v.user)
        this.isAuthenticated$.next(true);
      },
      error: e => {
        localStorage.removeItem("authenticated")
        this.authenticatedUser.next(undefined);
        this.isAuthenticated$.next(false);
      }
    }
    )
  }



}
