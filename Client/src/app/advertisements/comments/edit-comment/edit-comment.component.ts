import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { IComment } from 'src/app/models/IComment';
import { CommentService } from 'src/app/services/comment/comment.service';

@Component({
  selector: 'app-edit-comment',
  templateUrl: './edit-comment.component.html',
  styleUrls: ['./edit-comment.component.css']
})
export class EditCommentComponent implements OnInit {
  @Input() commentId: string;
  @Input() advertisementId: string;
  @Output() cancelEditForm = new EventEmitter<boolean>();
  
  comment: IComment;
  editCommentForm: FormGroup;

  constructor(
    private commentService: CommentService,
    private fb: FormBuilder,
    private toastrService: ToastrService) { 
      this.editCommentForm = this.fb.group({
        'content': ['', '']
      })
    }

  ngOnInit(): void {
    this.commentService.getById(this.commentId).subscribe(data => {
      this.comment = data;
      this.editCommentForm = this.fb.group({
        'content': [this.comment.content, [Validators.required, Validators.minLength(2), Validators.maxLength(300)]],
      })
    })
  }

  edit() {
    this.toastrService.clear();
    if (this.editCommentForm.invalid) {
      this.toastrService.error('Please populate correctly the comment content field!')
      return;
    }
    
    const commentToEdit = {
      content: this.editCommentForm.value['content']
    };

    this.commentService.edit(this.commentId, commentToEdit).subscribe(res => {
      this.toastrService.success('Successfully edited comment!');
      this.commentService.getAllByAdvertisementId(this.advertisementId).subscribe(data => {
        this.cancelEditForm.emit(true);
        this.commentService.loadComments(data);
      })
    })
  }
  
  get content() {
    return this.editCommentForm.get('content');
  }
}
