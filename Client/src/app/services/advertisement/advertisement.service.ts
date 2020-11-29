import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { IAdvertisement } from '../../models/IAdvertisement';

@Injectable({
  providedIn: 'root'
})
export class AdvertisementService {
  private enumsPath = environment.apiUrl + 'enums';
  private advertisementPath = environment.apiUrl + 'advertisements';
  private advertisementsOfUserPath = environment.apiUrl + 'advertisements/myAds';
  private advertisementsSearchPath = environment.apiUrl + 'advertisements/bySearch';
  private latestAdvertisementsPath = environment.apiUrl + 'advertisements/latest';
  private incrementViewsPath = environment.apiUrl + 'advertisements/incrementViews';

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

  getAdvertisement(advertisementId : string) : Observable<IAdvertisement> {
    return this.http.get<IAdvertisement>(this.advertisementPath + '/' + advertisementId);
  }

  getAdvertisementsOfUser(): Observable<IAdvertisement[]>{
    return this.http.get<IAdvertisement[]>(this.advertisementsOfUserPath);
  }

  incrementViews(advertisementId: string) : Observable<any> {
    return this.http.get(this.incrementViewsPath + '/' + advertisementId);
  }

  create(data) {
    return this.http.post(this.advertisementPath, data, { responseType: 'text' });
  }

  edit(advertisementId: string, data) {
    return this.http.put(this.advertisementPath + '/' + advertisementId, data)
  }

  delete(advertisementId) {
    return this.http.delete(this.advertisementPath + '/' + advertisementId);
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
