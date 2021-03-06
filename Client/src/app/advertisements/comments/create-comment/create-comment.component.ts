import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/services/auth/auth.service';
import { CommentService } from 'src/app/services/comment/comment.service';
import { mergeMap, map } from 'rxjs/operators';

@Component({
  selector: 'app-create-comment',
  templateUrl: './create-comment.component.html',
  styleUrls: ['./create-comment.component.css']
})
export class CreateCommentComponent implements OnInit {
  createCommentForm: FormGroup;
  @Input() advertisementId: string;

  get isLoggedIn(): boolean {
    return this.authService.isAuthenticated();
  }

  constructor(
    private fb: FormBuilder,
    private commentService: CommentService,
    private toastrService: ToastrService,
    private authService: AuthService) {
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

    this.commentService.create(commentToCreate).pipe(
      map(res => {
        this.toastrService.success('Successfully posted comment!');
        this.createCommentForm.reset();
      }), mergeMap(data => this.commentService.getAllByAdvertisementId(this.advertisementId))).subscribe(comments => {
        this.commentService.loadComments(comments);
      })
  }

  get content() {
    return this.createCommentForm.get('content');
  }
}
