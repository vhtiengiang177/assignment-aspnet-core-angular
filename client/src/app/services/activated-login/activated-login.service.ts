import { Injectable } from '@angular/core';
import { ActivatedRoute, ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../auth/auth.service';

@Injectable({
  providedIn: 'root'
})
export class ActivatedLogin implements CanActivate {

  constructor(private router: Router,
              private route: ActivatedRoute,
              private authService: AuthService) { }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    if(this.authService.isLoggedIn())
    {
      let returnUrl = this.route.snapshot.queryParamMap.get('returnUrl');
      this.router.navigate([ returnUrl || '/'])
      return false;
    }
    return true;
  }
}
