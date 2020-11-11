import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Brand } from '../../models/Brand';

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
}
