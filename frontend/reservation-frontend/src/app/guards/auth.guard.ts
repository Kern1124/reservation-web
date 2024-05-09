import { inject, } from '@angular/core';
import { CanActivateFn, Router} from '@angular/router';
import { AuthService } from '../services/auth.service';
import { map } from 'rxjs';


export function authGuard(): CanActivateFn {
    return () => {
      const authService = inject(AuthService);
      return authService.isAuthenticated$.pipe(
        map((value: boolean) => {
          return !value;
      }))
    };
  }
