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
        'doorsCount': ['', '']
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
      console.log(this.advertisements);
    });
  }

  onChangeBrand(brandId) {
    this.advertisementService.getModelsByBrandId(brandId).subscribe(models => {
      this.brandModels = models;
    });
  }

  search() {
    this.advertisementService.getAdvertisementsBySearch(this.searchForm.value).subscribe(ads => {
      this.advertisements = ads;
    });
  }

}
