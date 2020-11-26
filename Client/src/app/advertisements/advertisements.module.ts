import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ListAdvertisementsComponent } from './list-advertisements/list-advertisements.component';
import { DetailsAdvertisementComponent } from './details-advertisement/details-advertisement.component';
import { CreateAdvertisementComponent } from './create-advertisement/create-advertisement.component';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { MyAdvertisementsComponent } from './my-advertisements/my-advertisements.component';
import { MyAdvertisementInfoComponent } from './my-advertisement-info/my-advertisement-info.component';
import { RouterModule } from '@angular/router';
import { CreateCommentComponent } from './comments/create-comment/create-comment.component';
import { ListCommentsComponent } from './comments/list-comments/list-comments.component';
import { EditCommentComponent } from './comments/edit-comment/edit-comment.component';
import { CreateReplyComponent } from './replies/create-reply/create-reply.component';
import { ListRepliesComponent } from './replies/list-replies/list-replies.component';
import { EditReplyComponent } from './replies/edit-reply/edit-reply.component';
import { EditAdvertisementComponent } from './edit-advertisement/edit-advertisement.component';



@NgModule({
  declarations: [
    ListAdvertisementsComponent,
    DetailsAdvertisementComponent,
    CreateAdvertisementComponent,
    MyAdvertisementsComponent,
    MyAdvertisementInfoComponent,
    CreateCommentComponent,
    ListCommentsComponent,
    EditCommentComponent,
    CreateReplyComponent,
    ListRepliesComponent,
    EditReplyComponent,
    EditAdvertisementComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    SharedModule,
    ReactiveFormsModule,
    FormsModule,
    NgxDropzoneModule
  ]
})
export class AdvertisementsModule { }
