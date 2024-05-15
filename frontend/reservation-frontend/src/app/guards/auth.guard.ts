import { inject, } from '@angular/core';
import { CanActivateFn, Router} from '@angular/router';
import { AuthService } from '../services/auth.service';
import { last, map, shareReplay, skip, take, takeLast } from 'rxjs';


export function authGuard(): CanActivateFn {
    return () => {
      const authService = inject(AuthService)
      return authService.isAuthenticatedFn();
    };
  }
