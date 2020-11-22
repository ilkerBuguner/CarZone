import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { IReply } from 'src/app/models/IReply';
import { AuthService } from 'src/app/services/auth/auth.service';
import { ReplyService } from 'src/app/services/reply/reply.service';

@Component({
  selector: 'app-list-replies',
  templateUrl: './list-replies.component.html',
  styleUrls: ['./list-replies.component.css']
})
export class ListRepliesComponent implements OnInit {
  @Input() rootCommentId: string;
  currentUserId: string;
  selectedReplyId: string;
  isEditing: boolean;

  get isLoggedIn(): boolean {
    return this.authService.isAuthenticated();
  }
  get isAdmin(): boolean {
    return this.authService.isAdmin();
  }
  get replies(): IReply[] {
    return this.replyService.avaibleReplies;
  }

  constructor(
    private replyService: ReplyService,
    private authService: AuthService,
    private toastrService: ToastrService
  ) { }

  ngOnInit(): void {
    this.currentUserId = this.authService.getUserId();
    this.getAllReplies();
  }

  getAllReplies() {
    this.replyService.getAllByCommentId(this.rootCommentId).subscribe(data => {
      this.replyService.loadReplies(data);
    })
  }

  selectReply(replyId: string) {
    this.selectedReplyId = replyId;
  }

  unselectReply() {
    this.selectedReplyId = undefined;
  }

  delete(selectedReplyId) {
    this.replyService.delete(selectedReplyId).subscribe(res => {
      this.toastrService.success('Successfully deleted reply!');
      const closeButton = document.querySelector(".close-button") as HTMLElement;
      closeButton.click();
      this.getAllReplies();
    })
  }

  activateEditingAndSelectReply(replyId) {
    this.selectedReplyId = replyId;
    this.isEditing = true;
  }

  deactivateEditingAndUnselectReply() {
    this.selectedReplyId = undefined;
    this.isEditing = false;
  }
}
