import { Component, OnInit } from '@angular/core';
import { IBrand } from '../../models/IBrand';
import { IBrandModel } from '../../models/IBrandModel';
import { AdvertisementService } from '../../services/advertisement/advertisement.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { IAdvertisement } from '../../models/IAdvertisement';
import { Router } from '@angular/router';
import { BrandModelService } from '../../services/brandModel/brand-model.service';

@Component({
  selector: 'app-list-advertisements',
  templateUrl: './list-advertisements.component.html',
  styleUrls: ['./list-advertisements.component.css']
})
export class ListAdvertisementsComponent implements OnInit {
  searchForm: FormGroup;
  advertisements: IAdvertisement[];
  conditionTypes: string[];
  bodyTypes: string[];
  brands: IBrand[];
  brandModels: IBrandModel[];
  fuelTypes: string[];
  transmissionTypes: string[];
  colors: string[];
  locations: string[];
  euroStandards: string[];
  doorsCounts: string[];
  foundCarsCount: number;
  isSearching: boolean;

  constructor(private advertisementService: AdvertisementService,
    private brandModelService : BrandModelService,
    private fb: FormBuilder,
    private router: Router) {
      this.searchForm = this.fb.group( {
        'brandId': ['', ''], 'modelId': ['', ''], 'condition': ['', ''], 'bodyType': ['', ''],
        'fuelType': ['', ''], 'transmission': ['', ''], 'color': ['', ''], 'location': ['', ''],
        'euroStandard': ['', ''], 'doorsCount': ['', ''], 'minPrice': ['', ''], 'maxPrice': ['', ''],
        'minYear': ['', ''], 'maxYear': ['', ''], 'minHorsePower': ['', ''], 'maxHorsePower': ['', ''],
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
    this.isSearching = true;
    window.scrollTo({
      top: 580,
      behavior: "smooth"
    });

    this.advertisementService.setDefaultValuesOfEmptyInputs(this.searchForm)
    this.advertisementService.getAdvertisementsBySearch(this.searchForm.value).subscribe(ads => {
      this.advertisements = ads;
      this.foundCarsCount = this.advertisements.length;
      this.isSearching = false;
    });
  }

  navigateToAdvertisement(id) {
    this.router.navigate(["advertisement", id])
  }
}
