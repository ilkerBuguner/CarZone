import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Advertisement } from '../../models/Advertisement';

@Injectable({
  providedIn: 'root'
})
export class AdvertisementService {
  private enumsPath = environment.apiUrl + 'enums';
  private advertisementsSearchPath = environment.apiUrl + 'advertisements/bySearch';
  private latestAdvertisementsPath = environment.apiUrl + 'advertisements/latest';
  private advertisementDetailsPath = environment.apiUrl + 'advertisements';

  constructor(private http: HttpClient) { }

  getEnums(): Observable<any> {
    return this.http.get(this.enumsPath);
  }

  getAdvertisementsBySearch(data) : Observable<any> {
    return this.http.post(this.advertisementsSearchPath, data);
  }

  getLatestAdvertisements() : Observable<any> {
    return this.http.get(this.latestAdvertisementsPath);
  }

  getAdvertisement(advertisementId : string) : Observable<Advertisement> {
    return this.http.get<Advertisement>(this.advertisementDetailsPath + '/' + advertisementId);
  }

  setDefaultValuesOfEmptyInputs(form: FormGroup) {
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
