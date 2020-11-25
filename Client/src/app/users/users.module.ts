import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { ProfileComponent } from './profile/profile.component';
import { RouterModule } from '@angular/router';
import { EditUserComponent } from './edit-user/edit-user.component';
import { NgxDropzoneModule } from 'ngx-dropzone';



@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    ProfileComponent,
    EditUserComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    SharedModule,
    NgxDropzoneModule,
  ]
})
export class UsersModule { }
