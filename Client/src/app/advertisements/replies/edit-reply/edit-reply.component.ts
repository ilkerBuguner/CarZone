import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { IReply } from 'src/app/models/IReply';
import { ReplyService } from 'src/app/services/reply/reply.service';
import { mergeMap, map } from 'rxjs/operators';

@Component({
  selector: 'app-edit-reply',
  templateUrl: './edit-reply.component.html',
  styleUrls: ['./edit-reply.component.css']
})
export class EditReplyComponent implements OnInit {
  @Input() replyId: string;
  @Input() rootCommentId: string;
  @Output() cancelEditForm = new EventEmitter<boolean>();

  reply: IReply;
  editReplyForm: FormGroup;

  constructor(
    private replyService: ReplyService,
    private fb: FormBuilder,
    private toastrService: ToastrService) { 
    this.editReplyForm = this.fb.group({
      'content': ['', '']
    })
  }

  ngOnInit(): void {
    this.replyService.getById(this.replyId).subscribe(data => {
      this.reply = data;
      this.editReplyForm = this.fb.group({
        'content': [this.reply.content, [Validators.required, Validators.minLength(2), Validators.maxLength(300)]],
      })
    })
  }

  edit() {
    this.toastrService.clear();
    if (this.editReplyForm.invalid) {
      this.toastrService.error('Please populate correctly the content field!')
      return;
    }
    
    const replyToEdit = {
      content: this.editReplyForm.value['content']
    };

    this.replyService.edit(this.replyId, replyToEdit).pipe(
      map(res => {
        this.toastrService.success('Successfully edited reply!');
      }), mergeMap(data => this.replyService.getAllByCommentId(this.rootCommentId))).subscribe(replies => {
        this.cancelEditForm.emit(true);
        this.replyService.loadReplies(replies);
      })
  }

  closeEditForm() {
    this.cancelEditForm.emit(true);
  }

  get content() {
    return this.editReplyForm.get('content');
  }
}
