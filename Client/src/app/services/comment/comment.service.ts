import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { IComment } from 'src/app/models/IComment';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  avaibleComments: IComment[]
  private commentsPath = environment.apiUrl + 'comments';
  private commentByIdPath = environment.apiUrl + 'comments/byId';

  constructor(private http: HttpClient) { }

  loadComments(comments: IComment[]) {
    this.avaibleComments = comments;
  }

  create(data) {
    return this.http.post(this.commentsPath, data, { responseType: 'text' });
  }

  edit(commentId: string, data) {
    return this.http.put(this.commentsPath + '/' + commentId, data)
  }

  delete(commentId) {
    return this.http.delete(this.commentsPath + '/' + commentId);
  }

  getAllByAdvertisementId(advertisementId): Observable<IComment[]> {
    return this.http.get<IComment[]>(this.commentsPath + '/' + advertisementId);
  }

  getById(commentId) : Observable<IComment> {
    return this.http.get<IComment>(this.commentByIdPath + '/' + commentId);
  }
}
