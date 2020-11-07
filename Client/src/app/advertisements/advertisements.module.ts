import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListAdvertisementsComponent } from './list-advertisements/list-advertisements.component';
import { SharedModule } from '../shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [ListAdvertisementsComponent],
  imports: [
    CommonModule,
    SharedModule,
    ReactiveFormsModule
  ]
})
export class AdvertisementsModule { }
