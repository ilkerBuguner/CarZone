import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListBrandModelsComponent } from './brandModels/list-brand-models/list-brand-models.component';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    ListBrandModelsComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ]
})
export class AdminModule { }
