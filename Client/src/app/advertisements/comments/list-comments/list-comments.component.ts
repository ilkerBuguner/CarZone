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
  comments: IComment[];
  isAdmin: boolean;
  isLoggedIn: boolean;
  currentUserId: string;
  selectedCommentId: string;

  constructor(
    private commentService: CommentService,
    private authService: AuthService,
    private toastrService: ToastrService) { }

  ngOnInit(): void {
    if (this.authService.isAdmin()) {
      this.isAdmin = true;
    } else {
      this.isAdmin = false;
    }
    if (this.authService.isAuthenticated()) {
      this.isLoggedIn = true;
    } else {
      this.isLoggedIn = false;
    }
    this.currentUserId = this.authService.getUserId();
    this.getAllComments();
  }

  getAllComments() {
    this.commentService.getAllByAdvertisementId(this.advertisementId).subscribe(data => {
      this.comments = data;
      console.log(this.comments);
    })
  }

  selectComment(commentId) {
    this.selectedCommentId = commentId;
  }

  delete(commentId) {
    this.commentService.delete(commentId).subscribe(res => {
      this.toastrService.success('Successfully deleted comment!');
      const closeButton = document.querySelector(".close-button") as HTMLElement;
      closeButton.click();
      this.getAllComments();
    })
  }

}
