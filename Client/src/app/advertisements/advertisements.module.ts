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



@NgModule({
  declarations: [
    ListAdvertisementsComponent,
    DetailsAdvertisementComponent,
    CreateAdvertisementComponent,
    MyAdvertisementsComponent,
    MyAdvertisementInfoComponent
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
