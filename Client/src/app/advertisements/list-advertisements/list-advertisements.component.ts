import { Component, OnInit } from '@angular/core';
import { Brand } from '../../models/Brand';
import { BrandModel } from '../../models/BrandModel';
import { AdvertisementService } from '../../services/advertisement/advertisement.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Advertisement } from '../../models/Advertisement';
import { Router } from '@angular/router';
import { BrandModelService } from '../../services/brandModel/brand-model.service';

@Component({
  selector: 'app-list-advertisements',
  templateUrl: './list-advertisements.component.html',
  styleUrls: ['./list-advertisements.component.css']
})
export class ListAdvertisementsComponent implements OnInit {
  searchForm: FormGroup;
  advertisements: Advertisement[];
  conditionTypes: string[];
  bodyTypes: string[];
  brands: Brand[];
  brandModels: BrandModel[];
  fuelTypes: string[];
  transmissionTypes: string[];
  colors: string[];
  locations: string[];
  euroStandards: string[];
  doorsCounts: string[];
  foundCarsCount: number;

  constructor(private advertisementService: AdvertisementService,
    private brandModelService : BrandModelService,
    private fb: FormBuilder,
    private router: Router) {
      this.searchForm = this.fb.group( {
        'brandId': ['', ''],
        'modelId': ['', ''],
        'condition': ['', ''],
        'bodyType': ['', ''],
        'fuelType': ['', ''],
        'transmission': ['', ''],
        'color': ['', ''],
        'location': ['', ''],
        'euroStandard': ['', ''],
        'doorsCount': ['', ''],
        'minPrice': ['', ''],
        'maxPrice': ['', ''],
        'minYear': ['', ''],
        'maxYear': ['', ''],
        'minHorsePower': ['', ''],
        'maxHorsePower': ['', ''],
      })
     }

  ngOnInit(): void {
    this.advertisementService.getEnums().subscribe(enums => {
      this.conditionTypes = enums['conditionTypes'];
      this.bodyTypes = enums['bodyTypes'];
      this.fuelTypes = enums['fuelTypes'];
      this.transmissionTypes = enums['transmissionTypes'];
      this.colors = enums['colors'];
      this.locations = enums['locations'];
      this.euroStandards = enums['euroStandards'];
      this.doorsCounts = enums['doorsCounts'];
    });

    this.brandModelService.getBrands().subscribe(brands => {
      this.brands = this.brandModelService.sortBrandsByName(brands);
    });

    this.advertisementService.getLatestAdvertisements().subscribe(ads => {
      this.advertisements = ads;
    });
  }

  onChangeBrand(brandId) {
    this.brandModelService.getModelsByBrandId(brandId).subscribe(models => {
      this.brandModels = models;
    });
  }

  search() {
    
    window.scrollTo({
      top: 580,
      behavior: "smooth"
    });

    this.advertisementService.setDefaultValuesOfEmptyInputs(this.searchForm)
    this.advertisementService.getAdvertisementsBySearch(this.searchForm.value).subscribe(ads => {
      this.advertisements = ads;
      this.foundCarsCount = this.advertisements.length;
    });
  }

  navigateToAdvertisement(id) {
    this.router.navigate(["advertisements", id])
  }
}
