import { Component, OnInit } from '@angular/core';
import { Brand } from '../../models/Brand';
import { BrandModel } from '../../models/BrandModel';
import { AdvertisementService } from '../../services/advertisement/advertisement.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Advertisement } from '../../models/Advertisement';

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
  noResults: boolean;

  constructor(private advertisementService: AdvertisementService,
    private fb: FormBuilder) {
      this.searchForm = this.fb.group( {
        'brandId': ['', ''],
        'modelId': ['', ''],
        'condition': ['', ''],
        'bodyType': ['', ''],
        'fuelType': ['', ''],
        'transmissionType': ['', ''],
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

    this.advertisementService.getBrands().subscribe(brands => {
      this.brands = brands.sort((n1,n2) => {
        if (n1.name > n2.name) {
          return 1;
        }
        if (n1.name < n2.name) {
          return -1
        }

        return 0;
      });
    });

    this.advertisementService.getLatestAdvertisements().subscribe(ads => {
      this.advertisements = ads;
      if(this.advertisements.length == 0) {
        this.noResults = true;
      } else {
        this.noResults = false;
      }
    });
  }

  onChangeBrand(brandId) {
    this.advertisementService.getModelsByBrandId(brandId).subscribe(models => {
      this.brandModels = models;
    });
  }

  search() {
    this.setDefaultValuesOfEmpyInputs(this.searchForm)
    this.advertisementService.getAdvertisementsBySearch(this.searchForm.value).subscribe(ads => {
      this.advertisements = ads;
      if(this.advertisements.length == 0) {
        this.noResults = true;
      } else {
        this.noResults = false;
      }
    });
  }

  setDefaultValuesOfEmpyInputs(form: FormGroup) {
    debugger;
    if(form.value['minPrice'] == '' || form.value['minPrice'] == null) {
      form.patchValue({minPrice: 0});
    }
    if(form.value['maxPrice'] == '' || form.value['maxPrice'] == null) {
      form.patchValue({maxPrice: 0});
    }
    if(form.value['minYear'] == '' || form.value['minYear'] == null) {
      form.patchValue({minYear: 0});
    }
    if(form.value['maxYear'] == '' || form.value['maxYear'] == null) {
      form.patchValue({maxYear: 0});
    }
    if(form.value['minHorsePower'] == '' || form.value['minHorsePower'] == null) {
      form.patchValue({minHorsePower: 0});
    }
    if(form.value['maxHorsePower'] == '' || form.value['maxHorsePower'] == null) {
      form.patchValue({maxHorsePower: 0});
    }
  }
}
