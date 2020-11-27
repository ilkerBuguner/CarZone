import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UploadService {

  constructor(private http: HttpClient) { }
  private imagePath = environment.apiUrl + 'images';

  deleteimage(imageId) {
    return this.http.delete(this.imagePath + '/' + imageId);
  }

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
