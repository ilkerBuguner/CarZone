import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  isLoggedIn : boolean;
  isAdmin : boolean;
  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    if (this.authService.getToken()) {
      this.isLoggedIn = true
      if (this.authService.isAdmin()) {
        this.isAdmin = true;
      } else {
        this.isAdmin = false;
      }
    } else {
      this.isLoggedIn = false;
    }
  }
  
  logout() {
    this.authService.logout();
  }
}
