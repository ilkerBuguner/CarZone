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

@Component({
  selector: 'app-edit-advertisement',
  templateUrl: './edit-advertisement.component.html',
  styleUrls: ['./edit-advertisement.component.css']
})
export class EditAdvertisementComponent implements OnInit {
  editForm: FormGroup;
  advertisement: IAdvertisement;

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
  ) { 
    this.editForm = this.fb.group({
      'title': ['', [Validators.required, Validators.minLength(3), Validators.maxLength(30)]],
      'brandId': ['', [Validators.required]],
      'modelId': ['', [Validators.required]],
      'condition': ['', [Validators.required]],
      'bodyType': ['', [Validators.required]],
      'price': ['', [Validators.required]],
      'horsePower': ['', [Validators.required]],
      'year': ['', [Validators.required]],
      'mileage': ['', ''],
      'fuelType': ['', [Validators.required]],
      'transmission': ['', [Validators.required]],
      'color': ['', [Validators.required]],
      'location': ['', [Validators.required]],
      'euroStandard': ['', [Validators.required]],
      'doorsCount': ['', [Validators.required]],
      'description': ['', [Validators.required, Validators.minLength(5), Validators.maxLength(300)]],
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
      }),
      mergeMap(data => this.brandModelService.getModelsByBrandId(this.advertisement.car.brand.id))).subscribe(models => {
        this.brandModels = models;
      })

    // this.carService.getExteriors().subscribe(exteriors => {
    //   this.exteriors = exteriors;
    // })
    // this.carService.getComforts().subscribe(comforts => {
    //   this.comforts = comforts;
    // })
    // this.carService.getProtections().subscribe(protections => {
    //   this.protections = protections;
    // })
    // this.carService.getSafeties().subscribe(safeties => {
    //   this.safeties = safeties;
    // })
  }

  onChangeBrand(brandId) {
    this.brandModelService.getModelsByBrandId(brandId).subscribe(models => {
      this.brandModels = models;
    });
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
          doorsCount: this.editForm.value.doorsCount,
          carComforts: [],
          carExteriors: [],
          carProtections: [],
          carSafeties: [],
        }
      }
  
      // advertisementToSend.car['safeties'] = this.selectedSafetiesIds ? this.selectedSafetiesIds : [];
      // advertisementToSend.car['exteriors'] = this.selectedExteriorsIds ? this.selectedExteriorsIds : [];
      // advertisementToSend.car['comforts'] = this.selectedComfortsIds ? this.selectedComfortsIds : [];
      // advertisementToSend.car['protections'] = this.selectedProtectionsIds ? this.selectedProtectionsIds : [];
      
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
