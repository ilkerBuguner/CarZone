import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Brand } from '../../../models/Brand';
import { BrandModel } from '../../../models/BrandModel';
import { BrandModelService } from '../../../services/brandModel/brand-model.service';

@Component({
  selector: 'app-list-brand-models',
  templateUrl: './list-brand-models.component.html',
  styleUrls: ['./list-brand-models.component.css']
})
export class ListBrandModelsComponent implements OnInit {
  brands: Brand[];
  brandModels: BrandModel[];
  currentBrandId: string;
  currentModel: BrandModel;
  editBrandModelForm: FormGroup;
  noModelsFound: boolean = false;
  
  constructor(
    private brandModelService: BrandModelService,
    private fb: FormBuilder,
    private toastrService: ToastrService) { 
      this.editBrandModelForm = this.fb.group({
        'name': ['', [Validators.required, Validators.maxLength(30)]],
        'brandId': ['', Validators.required],
      })
    }

  ngOnInit(): void {
    this.brandModelService.getBrands().subscribe(brands => {
      this.brands = this.brandModelService.sortBrandsByName(brands);
    });
  }

  onChangeBrand(brandId) {
    this.noModelsFound = false;
    this.currentBrandId = brandId;
    this.brandModelService.getModelsByBrandId(brandId).subscribe(models => {
      this.brandModels = this.brandModelService.sortBrandModelsByName(models);
      if (this.brandModels.length == 0) {
        this.noModelsFound = true;
      }
    });
  }

  delete(modelId) {
    this.brandModelService.delete(modelId).subscribe(res => {
      this.toastrService.success('Successfully deleted model!');
      this.onChangeBrand(this.currentBrandId);
    })
  }

  edit() {
    if(this.editBrandModelForm.invalid) {
      this.toastrService.error('Please populate all fields correctly!')
      return;
    }
    this.brandModelService.edit(this.currentModel.id, this.editBrandModelForm.value).subscribe(res => {
      this.onChangeBrand(this.currentBrandId);
      this.toastrService.success(`Model edited successfully!`);
    })
  }

  fillFormWithData(modelId: string) {
    this.brandModelService.details(modelId).subscribe(res => {
      this.currentModel = res;
      this.editBrandModelForm = this.fb.group({
        'name': [this.currentModel.name],
        'brandId': [this.currentModel.brandId],
      })
    })
  }

  get name() {
    return this.editBrandModelForm.get('name');
  }

  get brandId() {
    return this.editBrandModelForm.get('brandId');
  }

}
