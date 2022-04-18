import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs/Observable';

import { AuthenticationService } from './services/authentication.service';
import { RouterService } from './services/router.service';

@Injectable()
export class CanActivateRouteGuard implements CanActivate {

  constructor(private authService: AuthenticationService, private routerService: RouterService) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
      // activate the routing based on user authentication
      const authPromise = this.authService.isUserAuthenticated(this.authService.getBearerToken());
      return authPromise.then(isAuthenticated => {
        if (!isAuthenticated) {
          // if the user is not authenticated, route to login page
          this.routerService.routeToLogin();
        }
        return isAuthenticated;
      });
    }
}
