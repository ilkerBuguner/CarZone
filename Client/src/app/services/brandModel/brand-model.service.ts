import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Brand } from '../../models/Brand';
import { BrandModel } from '../../models/BrandModel';

@Injectable({
  providedIn: 'root'
})
export class BrandModelService {
  private brandsPath = environment.apiUrl + 'brands';
  private brandModelsPath = environment.apiUrl + 'models';

  constructor(private http: HttpClient) { }

  getBrands(): Observable<any> {
    return this.http.get(this.brandsPath);
  }

  create(data) {
    return this.http.post(this.brandModelsPath, data, { responseType: 'text' });
  }

  edit(modelId: string, data: BrandModel) {
    return this.http.put(this.brandModelsPath + '/' + modelId, data)
  }
  
  delete(modelId) {
    return this.http.delete(this.brandModelsPath + '/' + modelId);
  }
  
  details(modelId: string): Observable<BrandModel> {
    return this.http.get<BrandModel>(this.brandModelsPath + '/' + modelId);
  }
  getModelsByBrandId(brandId): Observable<any> {
    return this.http.get(this.brandModelsPath + `/getByBrandId/${brandId}`)
  }

  sortBrandsByName(brands: Brand[]) : Brand[] {
    return brands.sort((n1,n2) => {
      if (n1.name > n2.name) {
        return 1;
      }
      if (n1.name < n2.name) {
        return -1
      }
      return 0;
    });
  }

  sortBrandModelsByName(models: BrandModel[]) : BrandModel[] {
    return models.sort((n1,n2) => {
      if (n1.name > n2.name) {
        return 1;
      }
      if (n1.name < n2.name) {
        return -1
      }
      return 0;
    });
  }
}
