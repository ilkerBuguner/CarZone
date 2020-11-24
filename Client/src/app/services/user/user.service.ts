import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IUser } from 'src/app/models/IUser';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private usersPath = environment.apiUrl + 'users';

  constructor(private http: HttpClient) { }

  details(userId: string): Observable<IUser> {
    return this.http.get<IUser>(this.usersPath + '/' + userId);
  }

  edit(userId: string, data) {
    return this.http.put(this.usersPath + '/' + userId, data);
  }
}
