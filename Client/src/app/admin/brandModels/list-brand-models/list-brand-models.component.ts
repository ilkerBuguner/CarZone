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
      this.brands = this.sortBrandsByName(brands);
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

  sortBrandsByName(brands: Brand[]) : Brand[] {
    return brands.sort((n1,n2) => {
      if (n1.name > n2.name) {
        return 1;
      }
      if (n1.name < n2.name) {
        return -1
      }
      return 0;
    });
  }

}
