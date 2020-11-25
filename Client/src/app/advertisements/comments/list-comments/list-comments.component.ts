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
  isEditing: boolean;
  isReplying: boolean;
  isRepliesShown: boolean;

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

  deleteComment(commentId) {
    this.commentService.delete(commentId).subscribe(res => {
      this.toastrService.success('Successfully deleted comment!');
      const closeButton = document.querySelector(".comment-close-button") as HTMLElement;
      closeButton.click();
      this.getAllComments();
    })
  }

  selectComment(commentId) {
    this.selectedCommentId = commentId;
  }

  unselectComment() {
    this.selectedCommentId = undefined;
  }

  activateEditingAndSelectComment(commentId) {
    this.selectedCommentId = commentId;
    this.isEditing = true;
  }

  deactivateEditingAndUnselectedComment() {
    this.selectedCommentId = undefined;
    this.isEditing = false;
  }

  activateIsReplying() {
    this.isReplying = true;
  }

  deactivateIsReplying() {
    this.isReplying = false;
  }

  activateReplyingAndSelectComment(commentId) {
    this.selectedCommentId = commentId;
    this.isReplying = true;
  }

  deactivateReplyingAndUnselectedComment() {
    this.selectedCommentId = undefined;
    this.isReplying = false;
  }

  showReplies() {
    this.isRepliesShown = true;
  }

  selectCommentAndToggleRepliesShowing(commentId) {
    if (this.isRepliesShown) {
      this.isRepliesShown = false
      this.selectedCommentId = undefined;
    } else {
      this.selectedCommentId = commentId;
      this.isRepliesShown = true;
    }
  }

}
