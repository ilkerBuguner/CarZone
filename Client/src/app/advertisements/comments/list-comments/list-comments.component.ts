import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { IComment } from 'src/app/models/IComment';
import { AuthService } from 'src/app/services/auth/auth.service';
import { CommentService } from 'src/app/services/comment/comment.service';

@Component({
  selector: 'app-list-comments',
  templateUrl: './list-comments.component.html',
  styleUrls: ['./list-comments.component.css']
})
export class ListCommentsComponent implements OnInit {
  @Input() advertisementId: string;
  currentUserId: string;
  selectedCommentId: string;

  get isLoggedIn(): boolean {
    return this.authService.isAuthenticated();
  }
  get isAdmin(): boolean {
    return this.authService.isAdmin();
  }
  get comments(): IComment[] {
    return this.commentService.avaibleComments;
  }

  constructor(
    private commentService: CommentService,
    private authService: AuthService,
    private toastrService: ToastrService) { }

  ngOnInit(): void {
    this.currentUserId = this.authService.getUserId();
    this.getAllComments();
  }

  getAllComments() {
    this.commentService.getAllByAdvertisementId(this.advertisementId).subscribe(data => {
      this.commentService.loadComments(data);
    })
  }

  delete(commentId) {
    this.commentService.delete(commentId).subscribe(res => {
      this.toastrService.success('Successfully deleted comment!');
      const closeButton = document.querySelector(".close-button") as HTMLElement;
      closeButton.click();
      this.getAllComments();
    })
  }

  selectComment(commentId) {
    this.selectedCommentId = commentId;
  }

  cancelEditForm() {
    this.selectedCommentId = undefined;
  }

}
