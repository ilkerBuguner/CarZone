import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IComment } from 'src/app/models/IComment';
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

  delete(commentId) {
    return this.http.delete(this.commentsPath + '/' + commentId);
  }

  getAllByAdvertisementId(advertisementId): Observable<IComment[]> {
    return this.http.get<IComment[]>(this.commentsPath + '/' + advertisementId);
  }
}
