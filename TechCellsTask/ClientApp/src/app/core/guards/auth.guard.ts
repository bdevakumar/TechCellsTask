import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {

    constructor(private router: Router) {
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        let username = localStorage.getItem('username');
        if (!username) {
            this.router.navigate(['login'], { replaceUrl: true, queryParams: { returnUrl: state.url } });
            return false;
        }
        return true;
    }
}