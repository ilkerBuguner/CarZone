import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { IBrand } from 'src/app/models/IBrand';
import { IBrandModel } from 'src/app/models/IBrandModel';
import { AdvertisementService } from 'src/app/services/advertisement/advertisement.service';
import { BrandModelService } from 'src/app/services/brandModel/brand-model.service';

@Component({
  selector: 'app-create-advertisement',
  templateUrl: './create-advertisement.component.html',
  styleUrls: ['./create-advertisement.component.css']
})
export class CreateAdvertisementComponent implements OnInit {
  createForm: FormGroup;
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

  constructor(
    private advertisementService: AdvertisementService,
    private brandModelService: BrandModelService,
    private fb: FormBuilder,
    private router: Router) { 
      this.createForm = this.fb.group( {
        'brandId': ['', ''], 'modelId': ['', ''], 'condition': ['', ''], 'bodyType': ['', ''],
        'fuelType': ['', ''], 'transmission': ['', ''], 'color': ['', ''], 'location': ['', ''],
        'euroStandard': ['', ''], 'doorsCount': ['', '']
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
  }

  onChangeBrand(brandId) {
    this.brandModelService.getModelsByBrandId(brandId).subscribe(models => {
      this.brandModels = models;
    });
  }

  create() {
    alert('You are in create function');
  }

}
