import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/services/auth/auth.service';
import { ReplyService } from 'src/app/services/reply/reply.service';
import { mergeMap, map } from 'rxjs/operators';

@Component({
  selector: 'app-create-reply',
  templateUrl: './create-reply.component.html',
  styleUrls: ['./create-reply.component.css']
})
export class CreateReplyComponent implements OnInit {
  createReplyForm: FormGroup;
  @Input() advertisementId: string;
  @Input() rootCommentId: string;
  @Output() stopReplying = new EventEmitter<boolean>();
  @Output() showReplies = new EventEmitter<boolean>();

  constructor(
    private fb: FormBuilder,
    private replyService: ReplyService,
    private toastrService: ToastrService) {
      this.createReplyForm = this.fb.group({
        'content': ['', [Validators.required, Validators.minLength(2), Validators.maxLength(300)]]
      })
    }

  ngOnInit(): void {
  }

  create() {
    this.toastrService.clear();
    if (this.createReplyForm.invalid) {
      this.toastrService.error('Please populate the reply content field!')
      return;
    }
    
    const commentToCreate = {
      content: this.createReplyForm.value['content'],
      advertisementId: this.advertisementId,
      rootCommentId: this.rootCommentId,
    };

    this.replyService.create(commentToCreate).pipe(
      map(res => {
        this.toastrService.success('Successfully posted reply!');
        this.createReplyForm.reset();
      }), mergeMap(data => this.replyService.getAllByCommentId(this.rootCommentId))).subscribe(replies => {
        this.stopReplying.emit(true);
        this.showReplies.emit(true);
        this.replyService.loadReplies(replies);
      })
  }

  hideReplyForm() {
    this.stopReplying.emit(true);
  }

  get content() {
    return this.createReplyForm.get('content');
  }

}
