import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CommentService } from 'src/app/services/comment/comment.service';

@Component({
  selector: 'app-create-comment',
  templateUrl: './create-comment.component.html',
  styleUrls: ['./create-comment.component.css']
})
export class CreateCommentComponent implements OnInit {
  createCommentForm: FormGroup;
  @Input() advertisementId: string;

  constructor(
    private fb: FormBuilder,
    private commentService: CommentService,
    private toastrService: ToastrService) {
      this.createCommentForm = this.fb.group({
        'content': ['', [Validators.required, Validators.minLength(2), Validators.maxLength(300)]]
    });
  }

  ngOnInit(): void {
  }

  create() {
    this.toastrService.clear();
    if (this.createCommentForm.invalid) {
      this.toastrService.error('Please populate the comment content field!')
      return;
    }

    const commentToCreate = {
      content: this.createCommentForm.value['content'],
      advertisementId: this.advertisementId
    };

    this.commentService.create(commentToCreate).subscribe(res => {
      this.toastrService.success('Successfully posted comment!');
    })
  }

  get content() {
    return this.createCommentForm.get('content');
  }
}
