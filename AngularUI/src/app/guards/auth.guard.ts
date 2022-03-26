import { Constants } from './../helper/constants.model';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router){}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      if (this.IsLoggedIn()){
        return true;
      }
      alert("you have not logged in")
      this.router.navigate(['/login']);
      return false;
  }
  
  IsLoggedIn() : boolean {
    return !!localStorage.getItem(Constants.USER_TOKEN);
  }
}
