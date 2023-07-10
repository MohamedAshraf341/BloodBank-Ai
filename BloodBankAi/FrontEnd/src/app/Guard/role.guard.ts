import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AcountService } from '../Services/acount.service'; 

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {
  constructor(private authService: AcountService, private router: Router) {}

  canActivate(): boolean {
    const userRoles = this.authService.getUserRoles();

    if (userRoles.includes('Member') 
    || userRoles.includes('Admin')
    || userRoles.includes('Moderator')
    || userRoles.includes('BankAdmin')
    || userRoles.includes('BankModerator')) {
      return true;
    } else {
      this.router.navigate(['/Login']);
      return false;
    }
  }
}