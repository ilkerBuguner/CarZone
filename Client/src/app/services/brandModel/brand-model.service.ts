import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { IBrand } from '../../models/IBrand';
import { IBrandModel } from '../../models/IBrandModel';

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

  edit(modelId: string, data: IBrandModel) {
    return this.http.put(this.brandModelsPath + '/' + modelId, data)
  }
  
  delete(modelId) {
    return this.http.delete(this.brandModelsPath + '/' + modelId);
  }
  
  details(modelId: string): Observable<IBrandModel> {
    return this.http.get<IBrandModel>(this.brandModelsPath + '/' + modelId);
  }
  
  getModelsByBrandId(brandId): Observable<any> {
    return this.http.get(this.brandModelsPath + `/getByBrandId/${brandId}`)
  }

  sortBrandsByName(brands: IBrand[]) : IBrand[] {
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

  sortBrandModelsByName(models: IBrandModel[]) : IBrandModel[] {
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
