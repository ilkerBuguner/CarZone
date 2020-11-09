import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Advertisement } from '../../models/Advertisement';

@Injectable({
  providedIn: 'root'
})
export class AdvertisementService {
  private enumsPath = environment.apiUrl + 'enums';
  private brandsPath = environment.apiUrl + 'brands';
  private brandModelsPath = environment.apiUrl + 'models';
  private advertisementsSearchPath = environment.apiUrl + 'advertisements/bySearch';
  private latestAdvertisementsPath = environment.apiUrl + 'advertisements/latest';
  private advertisementDetailsPath = environment.apiUrl + 'advertisements';

  constructor(private http: HttpClient) { }

  getEnums(): Observable<any> {
    return this.http.get(this.enumsPath);
  }

  getBrands(): Observable<any> {
    return this.http.get(this.brandsPath);
  }

  getModelsByBrandId(brandId): Observable<any> {
    return this.http.get(this.brandModelsPath + `/getByBrandId/${brandId}`)
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
}
