import { inject, } from '@angular/core';
import { CanActivateFn, Router} from '@angular/router';
import { AuthService } from '../services/auth.service';
import { last, map, shareReplay, skip, take, takeLast } from 'rxjs';

export function noAuthGuard(): CanActivateFn {
    return () => {
      const authService = inject(AuthService)
      const router = inject(Router)
      if (authService.isAuthenticatedFn()){
        router.navigate(["home"])
      }
      return !authService.isAuthenticatedFn();
    };
  }