import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { IBrand } from '../../../models/IBrand';
import { IBrandModel } from '../../../models/IBrandModel';
import { BrandModelService } from '../../../services/brandModel/brand-model.service';

@Component({
  selector: 'app-list-brand-models',
  templateUrl: './list-brand-models.component.html',
  styleUrls: ['./list-brand-models.component.css']
})
export class ListBrandModelsComponent implements OnInit {
  brands: IBrand[];
  brandModels: IBrandModel[];
  currentBrandId: string;
  currentModel: IBrandModel;
  selectedModelId: string;
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

  delete() {
    this.brandModelService.delete(this.selectedModelId).subscribe(res => {
      this.toastrService.success('Successfully deleted model!');
      const closeEditButton = document.querySelector(".delete-modal-close-button") as HTMLElement;
      closeEditButton.click();
      this.onChangeBrand(this.currentBrandId);
    })
  }

  edit() {
    if(this.editBrandModelForm.invalid) {
      this.toastrService.error('Please populate all fields correctly!')
      return;
    }
    this.brandModelService.edit(this.currentModel.id, this.editBrandModelForm.value).subscribe(res => {
      this.toastrService.success(`Model edited successfully!`);
      const closeEditButton = document.querySelector(".close-edit-button") as HTMLElement;
      closeEditButton.click();
      this.onChangeBrand(this.currentBrandId);
    })
  }

  fillFormWithData(modelId: string) {
    this.brandModelService.details(modelId).subscribe(res => {
      this.currentModel = res;
      this.editBrandModelForm = this.fb.group({
        'name': [this.currentModel.name, [Validators.required, Validators.maxLength(30)]],
        'brandId': [this.currentModel.brandId, [Validators.required]],
      })
    })
  }

  selectModelId(modelId: string) {
    this.selectedModelId = modelId;
  }

  get name() {
    return this.editBrandModelForm.get('name');
  }

  get brandId() {
    return this.editBrandModelForm.get('brandId');
  }
}
