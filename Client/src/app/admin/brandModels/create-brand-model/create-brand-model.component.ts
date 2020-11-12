import { Component, EventEmitter, Input, Output, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Brand } from 'src/app/models/Brand';
import { BrandModelService } from 'src/app/services/brandModel/brand-model.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-create-brand-model',
  templateUrl: './create-brand-model.component.html',
  styleUrls: ['./create-brand-model.component.css']
})
export class CreateBrandModelComponent implements OnInit {
  brands: Brand[];
  createBrandModelForm: FormGroup;
  @Output() onChangeBrandFunction = new EventEmitter<string>();

  constructor(
    private brandModelService: BrandModelService,
    private fb: FormBuilder,
    private toastrService: ToastrService) { 
      this.createBrandModelForm = this.fb.group({
        'name': ['', [Validators.required, Validators.maxLength(30)]],
        'brandId': ['', Validators.required]
      })
    }

  ngOnInit(): void {
    this.brandModelService.getBrands().subscribe(brands => {
      this.brands = this.brandModelService.sortBrandsByName(brands);
    });
  }

  create() {
    if (this.createBrandModelForm.invalid) {
      this.toastrService.error('Please populate all fields correctly!')
      return;
    }

    this.brandModelService.create(this.createBrandModelForm.value).subscribe(data => {
      const brandId = this.createBrandModelForm.value['brandId'];
      const modelName = this.createBrandModelForm.value['name'];
      this.onChangeBrandFunction.next(brandId);
      this.toastrService.success(`Model '${modelName}' created successfully!`);
      this.createBrandModelForm.reset();
    })
  }

  get name() {
    return this.createBrandModelForm.get('name');
  }

  get brandId() {
    return this.createBrandModelForm.get('brandId');
  }
}
