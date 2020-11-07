import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
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
}
