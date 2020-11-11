import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListBrandModelsComponent } from './brandModels/list-brand-models/list-brand-models.component';
import { SharedModule } from '../shared/shared.module';
import { CreateBrandModelComponent } from './brandModels/create-brand-model/create-brand-model.component';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    ListBrandModelsComponent,
    CreateBrandModelComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    ReactiveFormsModule
  ]
})
export class AdminModule { }
