import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';
import { ListAdvertisementsComponent } from './list-advertisements/list-advertisements.component';
import { DetailsAdvertisementComponent } from './details-advertisement/details-advertisement.component';
import { CreateAdvertisementComponent } from './create-advertisement/create-advertisement.component';



@NgModule({
  declarations: [
    ListAdvertisementsComponent,
    DetailsAdvertisementComponent,
    CreateAdvertisementComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    ReactiveFormsModule
  ]
})
export class AdvertisementsModule { }
