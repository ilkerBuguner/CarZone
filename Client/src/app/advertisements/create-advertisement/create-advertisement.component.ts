import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
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

  selectedComforts: IComfort[];
  selectedExteriors: IExterior[];
  selectedSafeties: ISafety[];
  selectedProtections: IProtection[];

  selectedComfortsIds: string[];
  selectedExteriorsIds: string[];
  selectedSafetiesIds: string[];
  selectedProtectionsIds: string[];

  constructor(
    private advertisementService: AdvertisementService,
    private brandModelService: BrandModelService,
    private carService: CarService,
    private fb: FormBuilder,
    private router: Router,
    private toastrService: ToastrService) { 
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
  changeCarComfortSelection() {
    this.selectedComforts = this.comforts.filter((value, index) => {
      return value.isChecked;
    });

    this.selectedComfortsIds = []
    this.comforts.forEach((value, index) => {
      if (value.isChecked) {
        this.selectedComfortsIds.push(value.id);
      }
    });
  }
  changeCarExteriorSelection() {
    this.selectedExteriors = this.exteriors.filter((value, index) => {
      return value.isChecked;
    });

    this.selectedExteriorsIds = []
    this.exteriors.forEach((value, index) => {
      if (value.isChecked) {
        this.selectedExteriorsIds.push(value.id);
      }
    });
  }
  changeCarProtectionSelection() {
    this.selectedProtections = this.protections.filter((value, index) => {
      return value.isChecked;
    });

    this.selectedProtectionsIds = []
    this.protections.forEach((value, index) => {
      if (value.isChecked) {
        this.selectedProtectionsIds.push(value.id);
      }
    });
  }
  changeCarSafetySelection() {
    this.selectedSafeties = this.safeties.filter((value, index) => {
      return value.isChecked;
    });
    this.selectedSafetiesIds = []
    this.safeties.forEach((value, index) => {
      if (value.isChecked) {
        this.selectedSafetiesIds.push(value.id);
      }
    });
  }

  create() {
    var advertisementToSend = {
      title: this.createForm.value.title,
      description: this.createForm.value.description,
      phoneNumber: this.createForm.value.phoneNumber.toString(),
      location: this.createForm.value.location,
      ImageURLs: [
        'https://cdn4.focus.bg/fakti/photos/medium/fdb/novata-toyota-yaris-shte-se-pravi-vav-francia-1.jpg'
      ],
      car: {
          brandId: this.createForm.value.brandId,
          modelId: this.createForm.value.modelId,
          bodyType: this.createForm.value.bodyType,
          price: this.createForm.value.price,
          fuelType: this.createForm.value.fuelType,
          horsePower: this.createForm.value.horsePower,
          transmission: this.createForm.value.transmission,
          year: this.createForm.value.year,
          mileage: this.createForm.value.mileage,
          color: this.createForm.value.color,
          condition: this.createForm.value.condition,
          euroStandard: this.createForm.value.euroStandard,
          doorsCount: this.createForm.value.doorsCount,
      }
    }

    advertisementToSend.car['safeties'] = this.selectedSafetiesIds ? this.selectedSafetiesIds : [];
    advertisementToSend.car['exteriors'] = this.selectedExteriorsIds ? this.selectedExteriorsIds : [];
    advertisementToSend.car['comforts'] = this.selectedComfortsIds ? this.selectedComfortsIds : [];
    advertisementToSend.car['protections'] = this.selectedProtectionsIds ? this.selectedProtectionsIds : [];

    this.advertisementService.create(advertisementToSend).subscribe(advertisementId => {
      this.toastrService.success('Successfully created new advertisement!')
      this.router.navigate(['advertisements']);
    })
  }

}