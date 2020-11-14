import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IComfort } from 'src/app/models/IComfort';
import { IExterior } from 'src/app/models/IExterior';
import { IProtection } from 'src/app/models/IProtection';
import { ISafety } from 'src/app/models/ISafety';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CarService {
  private comfortsPath = environment.apiUrl + 'comforts';
  private safetiesPath = environment.apiUrl + 'safeties';
  private exteriorsPath = environment.apiUrl + 'exteriors';
  private protectionsPath = environment.apiUrl + 'protections';

  constructor(private http: HttpClient) { }

  getComforts() : Observable<IComfort[]> {
    return this.http.get<IComfort[]>(this.comfortsPath);
  }

  getSafeties() : Observable<ISafety[]> {
    return this.http.get<ISafety[]>(this.safetiesPath);
  }

  getExteriors() : Observable<IExterior[]> {
    return this.http.get<IExterior[]>(this.exteriorsPath);
  }

  getProtections() : Observable<IProtection[]> {
    return this.http.get<IProtection[]>(this.protectionsPath);
  } 
}
