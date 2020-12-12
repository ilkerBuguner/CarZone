import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
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
import { UploadService } from 'src/app/services/upload/upload.service';
import { map, mergeMap } from 'rxjs/operators';
import { IAdvertisement } from 'src/app/models/IAdvertisement';
import { Observable, combineLatest } from 'rxjs';

@Component({
  selector: 'app-edit-advertisement',
  templateUrl: './edit-advertisement.component.html',
  styleUrls: ['./edit-advertisement.component.css']
})
export class EditAdvertisementComponent implements OnInit {
  editForm: FormGroup;
  advertisement: IAdvertisement;
  isLoading: boolean;

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
  files: File[] = [];
  imageURLs: string[] = [];
  isButtonDisabled: boolean;

  comforts: IComfort[];
  exteriors: IExterior[];
  safeties: ISafety[];
  protections: IProtection[];

  constructor(
    private advertisementService: AdvertisementService,
    private brandModelService: BrandModelService,
    private carService: CarService,
    private fb: FormBuilder,
    private router: Router,
    private toastrService: ToastrService,
    private uploadService: UploadService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.isLoading = true;
    combineLatest([
      this.carService.getExteriors(),
      this.carService.getComforts(),
      this.carService.getProtections(),
      this.carService.getSafeties()
    ]).subscribe(([exteriors, comforts, protections, safeties]) => {
        this.exteriors = exteriors;
        this.comforts = comforts;
        this.protections = protections;
        this.safeties = safeties;

        this.route.params.pipe(
          map(params => {
            const id = params['id'];
            return id;
          }), 
          mergeMap(id => this.advertisementService.getAdvertisement(id))).pipe(
            map(res => {
              this.advertisement = res;
              this.editForm = this.fb.group({
                'title': [this.advertisement.title, [Validators.required, Validators.minLength(3), Validators.maxLength(30)]],
                'brandId': [this.advertisement.car.brand.id, [Validators.required]],
                'modelId': [this.advertisement.car.model.id, [Validators.required]],
                'condition': [this.advertisement.car.condition, [Validators.required]],
                'bodyType': [this.advertisement.car.bodyType, [Validators.required]],
                'price': [this.advertisement.car.price, [Validators.required]],
                'horsePower': [this.advertisement.car.horsePower, [Validators.required]],
                'year': [this.advertisement.car.year, [Validators.required]],
                'mileage': [this.advertisement.car.mileage, ''],
                'fuelType': [this.advertisement.car.fuelType, [Validators.required]],
                'transmission': [this.advertisement.car.transmission, [Validators.required]],
                'color': [this.advertisement.car.color, [Validators.required]],
                'location': [this.advertisement.location, [Validators.required]],
                'euroStandard': [this.advertisement.car.euroStandard, [Validators.required]],
                'doorsCount': [this.advertisement.car.doorsCount, [Validators.required]],
                'description': [this.advertisement.description, [Validators.required, Validators.minLength(5), Validators.maxLength(300)]],
              })
      
              this.fillCarExtras();
              this.isLoading = false;
            }),
            mergeMap(data => this.brandModelService.getModelsByBrandId(this.advertisement.car.brand.id))).subscribe(models => {
              this.brandModels = models;
            })
      })

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

  fillCarExtras() {
    for (const comfort of this.comforts) {
      for (const carComfort of this.advertisement.car.comforts) {
        if(comfort.name == carComfort.toString()) {
          comfort.isChecked = true;
        }
      }
    }
    for (const exterior of this.exteriors) {
      for (const carExterior of this.advertisement.car.exteriors) {
        if(exterior.name == carExterior.toString()) {
          exterior.isChecked = true;
        }
      }
    }
    for (const safety of this.safeties) {
      for (const carSafety of this.advertisement.car.safeties) {
        if(safety.name == carSafety.toString()) {
          safety.isChecked = true;
        }
      }
    }
    for (const protection of this.protections) {
      for (const carProtection of this.advertisement.car.protections) {
        if(protection.name == carProtection.toString()) {
          protection.isChecked = true;
        }
      }
    }
  }

  deleteImage(imageId: string) {
    this.uploadService.deleteimage(imageId).pipe(
      map(data => {
        this.toastrService.success('Successfully deleted image!')
      }),
      mergeMap(data => this.advertisementService.getAdvertisement(this.advertisement.id))).subscribe(res => {
        this.advertisement = res;
      })
  }

  onSelectImage(event) {
    this.files.push(...event.addedFiles);
  }

  onRemoveImage(event) {
    this.files.splice(this.files.indexOf(event), 1);
  }

  edit() {
    if (this.editForm.invalid) {
      this.toastrService.error('Please populate all requried fields and selects!');
      return;
    }
    this.isButtonDisabled = true;
    
    this.toastrService.info('Editing...')
    var promises = [];
    if (this.files.length > 0) {
      for (const file_data of this.files) {
        const data = new FormData();
        data.append('file', file_data);
        data.append('upload_preset', 'Carzone_cloudinary');
        data.append('cloud_name', 'doyjshrjs');

        let httpData = this.uploadService.uploadImageToCloudinary(data)
        promises.push(httpData);
      }
    }

    Promise.all(promises).then((results) => {
      for (const result of results) {
        this.imageURLs.push(result['secure_url']);
      }
      
      var advertisementToSend = {
        title: this.editForm.value.title,
        description: this.editForm.value.description,
        // phoneNumber: this.editForm.value.phoneNumber.toString(),
        // location: this.editForm.value.location,
        imageURLs: this.imageURLs ? this.imageURLs : [],
        car: {
          brandId: this.editForm.value.brandId,
          modelId: this.editForm.value.modelId,
          bodyType: this.editForm.value.bodyType,
          price: this.editForm.value.price,
          fuelType: this.editForm.value.fuelType,
          horsePower: this.editForm.value.horsePower,
          transmission: this.editForm.value.transmission,
          year: this.editForm.value.year,
          mileage: this.editForm.value.mileage ? this.editForm.value.mileage : 0,
          color: this.editForm.value.color,
          condition: this.editForm.value.condition,
          euroStandard: this.editForm.value.euroStandard,
          doorsCount: this.editForm.value.doorsCount
        }
      }
  
      advertisementToSend.car['carSafeties'] = this.safeties ? this.safeties : [];
      advertisementToSend.car['carExteriors'] = this.exteriors ? this.exteriors : [];
      advertisementToSend.car['carComforts'] = this.comforts ? this.comforts : [];
      advertisementToSend.car['carProtections'] = this.protections ? this.protections : [];
      
      this.advertisementService.edit(this.advertisement.id, advertisementToSend).subscribe(res => {
        this.toastrService.clear();
        this.toastrService.success('Successfully edited advertisement');
        this.router.navigate(["advertisement", this.advertisement.id]);
      })
    })
  }

  get title() {
    return this.editForm.get('title');
  }
  get description() {
    return this.editForm.get('description');
  }
  get brandId() {
    return this.editForm.get('brandId');
  }
  get modelId() {
    return this.editForm.get('modelId');
  }
  get bodyType() {
    return this.editForm.get('bodyType');
  }
  get transmission() {
    return this.editForm.get('transmission');
  }
  get price() {
    return this.editForm.get('price');
  }
  get horsePower() {
    return this.editForm.get('horsePower');
  }
  get year() {
    return this.editForm.get('year');
  }
  get fuelType() {
    return this.editForm.get('fuelType');
  }
  get location() {
    return this.editForm.get('location');
  }
  get color() {
    return this.editForm.get('color');
  }
  get euroStandard() {
    return this.editForm.get('euroStandard');
  }
  get condition() {
    return this.editForm.get('condition');
  }
  get doorsCount() {
    return this.editForm.get('doorsCount');
  }
}
