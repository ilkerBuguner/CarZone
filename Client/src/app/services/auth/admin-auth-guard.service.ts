import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AdminAuthGuardService implements CanActivate{

  constructor(private authService: AuthService, private router: Router) { }

  canActivate(): boolean {
    if (this.authService.isAdmin()) {
      return true;
    } else {
      alert('Only admins have access to this page!')
      this.router.navigate(['advertisements']);
      return false;
    }
  }
}
