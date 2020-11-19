import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  private commentsPath = environment.apiUrl + 'comments';

  constructor(private http: HttpClient) { }

  create(data) {
    return this.http.post(this.commentsPath, data, { responseType: 'text' });
  }
}
