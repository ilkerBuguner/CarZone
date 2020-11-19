import { Component, Input, OnInit } from '@angular/core';
import { IComment } from 'src/app/models/IComment';
import { CommentService } from 'src/app/services/comment/comment.service';

@Component({
  selector: 'app-list-comments',
  templateUrl: './list-comments.component.html',
  styleUrls: ['./list-comments.component.css']
})
export class ListCommentsComponent implements OnInit {
  @Input() advertisementId: string;
  comments: IComment[];
  constructor(private commentSertive: CommentService) { }

  ngOnInit(): void {
    this.getAllComments();
  }

  getAllComments() {
    this.commentSertive.getAllByAdvertisementId(this.advertisementId).subscribe(data => {
      this.comments = data;
    })
  }

}
