import { Component, OnInit } from '@angular/core';
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
  noModelsFound: boolean = false;
  
  constructor(
    private brandModelService: BrandModelService,
    private toastrService: ToastrService) { }

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

}
