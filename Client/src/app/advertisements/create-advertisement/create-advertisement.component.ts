import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { IBrand } from 'src/app/models/IBrand';
import { IBrandModel } from 'src/app/models/IBrandModel';
import { IComfort } from 'src/app/models/IComfort';
import { IExterior } from 'src/app/models/IExterior';
import { IProtection } from 'src/app/models/IProtection';
import { ISafety } from 'src/app/models/ISafety';
import { AdvertisementService } from 'src/app/services/advertisement/advertisement.service';
import { BrandModelService } from 'src/app/services/brandModel/brand-model.service';
import { CarService } from 'src/app/services/car/car.service';

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
  comforts: IComfort[];
  exteriors: IExterior[];
  safeties: ISafety[];
  protections: IProtection[];

  constructor(
    private advertisementService: AdvertisementService,
    private brandModelService: BrandModelService,
    private carService: CarService,
    private fb: FormBuilder,
    private router: Router) { 
      this.createForm = this.fb.group( {
        'title': ['', ''],
        'brandId': ['', ''],
        'modelId': ['', ''],
        'condition': ['', ''],
        'bodyType': ['', ''],
        'price': ['', ''],
        'horsePower': ['', ''],
        'year': ['', ''],
        'mileage': ['', ''],
        'fuelType': ['', ''],
        'transmission': ['', ''],
        'color': ['', ''],
        'location': ['', ''],
        'euroStandard': ['', ''],
        'doorsCount': ['', ''],
        'description': ['', ''],
        'email': ['', ''],
        'phoneNumber': ['', ''],
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

    this.carService.getExteriors().subscribe(exteriors => {
      this.exteriors = exteriors;
    })
    this.carService.getComforts().subscribe(comforts => {
      this.comforts = comforts;
    })
    this.carService.getProtections().subscribe(protections => {
      this.protections = protections;
    })
    this.carService.getSafeties().subscribe(safeties => {
      this.safeties = safeties;
    })
  }

  onChangeBrand(brandId) {
    this.brandModelService.getModelsByBrandId(brandId).subscribe(models => {
      this.brandModels = models;
    });
  }

  create() {
    console.log(this.createForm.value);
  }

}
