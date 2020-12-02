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

  selectedBrandName: string;
  selectedModelName: string;

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

  onChangeBrand(event) {
    const brandId = event.target.value;
    const brandName = event.target.options[event.target.options.selectedIndex].text;
    this.selectedBrandName = brandName
    if(brandId == "") {
      return;
    }
    
    this.brandModelService.getModelsByBrandId(brandId).subscribe(models => {
      this.brandModels = models;
      this.searchForm.controls.modelId.setValue('');
    });
  }

  onChangeModel(event) {
    const modelName = event.target.options[event.target.options.selectedIndex].text;
    this.selectedModelName = modelName;
  }

  search() {
    this.isSearching = true;
    window.scrollTo({
      top: 700,
      behavior: "smooth"
    });

    this.advertisementService.setDefaultValuesOfEmptyInputs(this.searchForm)
    this.advertisementService.getAdvertisementsBySearch(this.searchForm.value).subscribe(ads => {
      this.advertisements = ads;
      this.foundCarsCount = this.advertisements.length;
      this.isSearching = false;
    });
  }

  removeSearchConstraint(constraintType) {
    if (constraintType == 'brand') {
      this.searchForm.controls.brandId.setValue('');
      this.searchForm.controls.modelId.setValue('');
      this.selectedModelName = '';
      this.selectedBrandName = '';
    } else if (constraintType == 'model') {
      this.searchForm.controls.modelId.setValue('');
      this.selectedModelName = '';
    } else if (constraintType == 'condition') {
      this.searchForm.controls.condition.setValue('');
    } else if (constraintType == 'bodyType') {
      this.searchForm.controls.bodyType.setValue('');
    } else if (constraintType == 'fuelType') {
      this.searchForm.controls.fuelType.setValue('');
    } else if (constraintType == 'transmission') {
      this.searchForm.controls.transmission.setValue('');
    } else if (constraintType == 'color') {
      this.searchForm.controls.color.setValue('');
    } else if (constraintType == 'location') {
      this.searchForm.controls.location.setValue('');
    } else if (constraintType == 'euroStandard') {
      this.searchForm.controls.euroStandard.setValue('');
    } else if (constraintType == 'doorsCount') {
      this.searchForm.controls.doorsCount.setValue('');
    } else if (constraintType == 'minPrice') {
      this.searchForm.controls.minPrice.setValue('');
    } else if (constraintType == 'maxPrice') {
      this.searchForm.controls.maxPrice.setValue('');
    } else if (constraintType == 'minYear') {
      this.searchForm.controls.minYear.setValue('');
    } else if (constraintType == 'maxYear') {
      this.searchForm.controls.maxYear.setValue('');
    } else if (constraintType == 'minHorsePower') {
      this.searchForm.controls.minHorsePower.setValue('');
    } else if (constraintType == 'maxHorsePower') {
      this.searchForm.controls.maxHorsePower.setValue('');
    }
    this.search();
  }
}
