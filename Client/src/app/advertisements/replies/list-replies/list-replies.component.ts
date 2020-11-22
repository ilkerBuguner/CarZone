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

}
