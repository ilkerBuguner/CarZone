import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  get isLoggedIn(): boolean {
    return this.authService.isAuthenticated();
  }
  get isAdmin(): boolean {
    return this.authService.isAdmin();
  }
  get userId(): string {
    return this.authService.getUserId();
  }
  get username(): string {
    return this.authService.getUsername();
  }

  constructor(
    private authService: AuthService,
    private router: Router) { }

  ngOnInit(): void {
  }
  
  logout() {
    this.authService.logout();
  }
}
