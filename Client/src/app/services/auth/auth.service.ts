import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loginPath = environment.apiUrl + 'identity/login';
  private registerPath = environment.apiUrl + 'identity/register';

  constructor(private http: HttpClient, private router: Router) { }

  login(data): Observable<any> {
    return this.http.post(this.loginPath, data);
  }

  register(data) : Observable<any> {
    return this.http.post(this.registerPath, data);
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('isAdmin');
    this.router.navigate(["login"]);
  }

  deleteToken() {
    localStorage.removeItem('token');
  }

  saveToken(token) {
    localStorage.setItem('token', token);
  }

  getToken() {
    return localStorage.getItem('token');
  }
  
  saveIsAdmin(isAdmin) {
    localStorage.setItem('isAdmin', isAdmin);
  }

  getIsAdmin(){
    return localStorage.getItem('isAdmin');
  }

  getUserId(): string {
    return localStorage.getItem('userId');
  }

  setUserInfo(userId: string, username: string) {
    localStorage.setItem('userId', userId);
    localStorage.setItem('username', username);
    return;
  }

  isAuthenticated(): boolean {
    if (this.getToken()) {
      return true;
    }
    return false;
  }

  isAdmin(): boolean {
    var isAdmin = this.getIsAdmin();
    if (isAdmin == 'true') {
      return true;
    }
    return false;
  }
}
