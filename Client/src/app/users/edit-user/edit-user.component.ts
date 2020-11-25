import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { IUser } from 'src/app/models/IUser';
import { AdvertisementService } from 'src/app/services/advertisement/advertisement.service';
import { AuthService } from 'src/app/services/auth/auth.service';
import { UploadService } from 'src/app/services/upload/upload.service';
import { UserService } from 'src/app/services/user/user.service';
import { map, mergeMap } from 'rxjs/operators';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css']
})
export class EditUserComponent implements OnInit {
  editForm: FormGroup;
  locations: string[];
  genders: string[];
  user: IUser;
  userId: string;
  files: File[] = [];

  constructor(
    private advertisementService: AdvertisementService,
    private userService: UserService,
    private authService: AuthService,
    private fb: FormBuilder,
    private router: Router,
    private uploadService: UploadService,
    private toastrService: ToastrService,
  ) {
    this.editForm = this.fb.group({
      'username': ['', [Validators.required, Validators.minLength(3), Validators.maxLength(30)]],
      'fullName': ['', [Validators.required, Validators.minLength(5), Validators.maxLength(40)]],
      'email': ['', [Validators.required]],
      'phoneNumber': ['', ''],
      'location': ['', ''],
      'gender': ['', ''],
    })
    this.editForm.controls['username'].disable();

    this.userId = this.authService.getUserId();
  }

  ngOnInit(): void {
    this.advertisementService.getEnums().subscribe(enums => {
      this.locations = enums['locations'];
      this.genders = enums['genders'];
    });

    this.userService.details(this.userId).subscribe(data => {
      this.user = data;
      this.editForm = this.fb.group({
        'username': [this.user.username, [Validators.required, Validators.minLength(3), Validators.maxLength(30)]],
        'fullName': [this.user.fullName, [Validators.required, Validators.minLength(5), Validators.maxLength(40)]],
        'email': [this.user.email, [Validators.required]],
        'phoneNumber': [this.user.phoneNumber, ''],
        'location': [this.user.location, ''],
        'gender': [this.user.gender, ''],
      })
      this.editForm.controls['username'].disable();
    })
  }

  onSelect(event) {
    this.files = [];
    this.files.push(...event.addedFiles);
  }
   
  onRemove(event) {
    this.files.splice(this.files.indexOf(event), 1);
  }

  resetUserProfilePicture() {
    this.userService.resetProfilePicture().pipe(
      mergeMap(res => this.userService.details(this.userId))).subscribe(data => {
        this.user = data;
      })
  }
  
  edit() {
    if(this.editForm.invalid) {
      this.toastrService.error('Please populate all fields correctly!')
      return;
    }
    this.toastrService.info('Loading...')

    const editedUserToSend = {
      username: this.editForm.value['username'],
      email: this.editForm.value['email'],
      fullName: this.editForm.value['fullName'],
      phoneNumber: (this.editForm.value['phoneNumber'])?.toString(),
      location: this.editForm.value['location'],
      gender: this.editForm.value['gender'],
    }

    if (this.files.length > 0) {
      const file_data = this.files[0];
      const data = new FormData();
      data.append('file', file_data);
      data.append('upload_preset', 'Carzone_cloudinary');
      data.append('cloud_name', 'doyjshrjs');

      this.uploadService.uploadImageToClodinaryAsObservable(data).pipe(
        map(data => {
          editedUserToSend["profilePictureUrl"] = data['secure_url'];
        }), mergeMap(res => this.userService.edit(this.userId, editedUserToSend))).subscribe(data => {
          this.toastrService.clear();
          this.toastrService.success('Successfully edited user information!');
          this.router.navigate(["user", this.userId]);
        })
    } else {
      this.userService.edit(this.userId, editedUserToSend).subscribe(data => {
        this.toastrService.clear();
        this.toastrService.success('Successfully edited user information!')
        this.router.navigate(["user", this.userId])
      });
    }
  }

  get username() {
    return this.editForm.get('username');
  }
  get email() {
    return this.editForm.get('email');
  }
  get fullName() {
    return this.editForm.get('fullName');
  }
  get phoneNumber() {
    return this.editForm.get('phoneNumber');
  }
  get location() {
    return this.editForm.get('location');
  }
  get gender() {
    return this.editForm.get('gender');
  }
}
