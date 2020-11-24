import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ListAdvertisementsComponent } from './advertisements/list-advertisements/list-advertisements.component';
import { DetailsAdvertisementComponent } from './advertisements/details-advertisement/details-advertisement.component';
import { LoginComponent } from './users/login/login.component';
import { RegisterComponent } from './users/register/register.component';
import { ListBrandModelsComponent } from './admin/brandModels/list-brand-models/list-brand-models.component';
import { AuthGuardService } from './services/auth/auth-guard.service';
import { AdminAuthGuardService } from './services/auth/admin-auth-guard.service';
import { CreateAdvertisementComponent } from './advertisements/create-advertisement/create-advertisement.component';
import { MyAdvertisementsComponent } from './advertisements/my-advertisements/my-advertisements.component';
import { NotFoundComponent } from './shared/not-found/not-found.component';
import { ProfileComponent } from './users/profile/profile.component';
import { EditUserComponent } from './users/edit-user/edit-user.component';

const routes: Routes = [
  { path: '', component: ListAdvertisementsComponent},
  { path: 'login', component: LoginComponent},
  { path: 'register', component: RegisterComponent},
  { path: 'user/:id', component: ProfileComponent},
  { path: 'advertisements', component: ListAdvertisementsComponent},
  { path: 'advertisement/:id', component: DetailsAdvertisementComponent},
  { path: 'profile/edit', component: EditUserComponent, canActivate: [AuthGuardService]},
  { path: 'myAds', component: MyAdvertisementsComponent, canActivate: [AuthGuardService]},
  { path: 'advertisements/create', component: CreateAdvertisementComponent, canActivate: [AuthGuardService]},
  { path: 'admin/brandModels', component: ListBrandModelsComponent, canActivate: [AuthGuardService, AdminAuthGuardService]},
  { path: '**', component: NotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    scrollPositionRestoration: 'enabled'
  })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
