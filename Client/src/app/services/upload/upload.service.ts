import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UploadService {

  constructor(private http: HttpClient) { }

  uploadImageToCloudinary(vals) {
    let data = vals;

    return this.http.post(
      'https://api.cloudinary.com/v1_1/doyjshrjs/image/upload',
      data
    ).toPromise();
  }

  uploadImageToClodinaryAsObservable(vals) {
    let data = vals;

    return this.http.post(
      'https://api.cloudinary.com/v1_1/doyjshrjs/image/upload',
      data
    );
  }
}
