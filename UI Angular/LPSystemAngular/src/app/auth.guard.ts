import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  
  constructor(private router: Router) {}

  // method of route protection by using auth guard canActive
  canActivate(
    activatedRoute: ActivatedRouteSnapshot,
  ): boolean {
    let expectedRoles = activatedRoute.data.expectedRoles;
    let userData = JSON.parse(localStorage.getItem('user'));
    let expectedRole = null;
    
    // If no user data, redirect to login
    if (!userData) {
      alert('Please login to access this page');
      this.router.navigate(['/login']);
      return false;
    }
    
    // Check if user role matches any expected role
    if (expectedRoles && expectedRoles.length > 0) {
      expectedRoles.forEach(role => {
        if (role === userData.UserRole) {
          expectedRole = userData.UserRole;
        }
      });
    }
    
    if (expectedRole) {
      return true;
    } else {
      alert('You do not have permission to access this page');
      this.router.navigate(['/home']);
      return false;
    }
  }
}
