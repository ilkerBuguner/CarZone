import { Component, OnInit } from '@angular/core';
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
  noModelsFound: boolean = false;
  
  constructor(private brandModelService: BrandModelService) { }

  ngOnInit(): void {
    this.brandModelService.getBrands().subscribe(brands => {
      this.brands = this.brandModelService.sortBrandsByName(brands);
    });
  }

  onChangeBrand(brandId) {
    this.noModelsFound = false;
    this.brandModelService.getModelsByBrandId(brandId).subscribe(models => {
      this.brandModels = models;
      if (this.brandModels.length == 0) {
        this.noModelsFound = true;
      }
    });
  }

}
