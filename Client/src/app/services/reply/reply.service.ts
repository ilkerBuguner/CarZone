import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IReply } from 'src/app/models/IReply';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReplyService {
  avaibleReplies: IReply[]
  private repliesPath = environment.apiUrl + 'replies';
  private replyByIdPath = environment.apiUrl + 'replies/byId';

  constructor(private http: HttpClient) { }

  loadReplies(comments: IReply[]) {
    this.avaibleReplies = comments;
  }

  create(data) {
    return this.http.post(this.repliesPath, data, { responseType: 'text' });
  }

  edit(replyId: string, data) {
    return this.http.put(this.repliesPath + '/' + replyId, data)
  }

  delete(replyId) {
    return this.http.delete(this.repliesPath + '/' + replyId);
  }

  getAllByCommentId(commentId): Observable<IReply[]> {
    return this.http.get<IReply[]>(this.repliesPath + '/' + commentId);
  }

  getById(replyId) : Observable<IReply> {
    return this.http.get<IReply>(this.replyByIdPath + '/' + replyId);
  }
}
