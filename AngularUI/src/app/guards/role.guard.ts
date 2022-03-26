import { Constants } from './../helper/constants.model';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      let role = localStorage.getItem(Constants.USER_ROLES);
      if (role === null || role === undefined) {
        alert("you don't have permission to access");
        return false;
      }
      else
      {
        var roles = JSON.parse(role);
        console.log(roles);
        for (var i = 0; i < roles.length; i++) {
          if (roles[i] === 'Admin') {
            return true;
          }
        }
      }
      alert("you don't have permission to access");
      return false;
  }
  
}
