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

const routes: Routes = [
  { path: '', component: ListAdvertisementsComponent},
  { path: 'login', component: LoginComponent},
  { path: 'register', component: RegisterComponent},
  { path: 'advertisements', component: ListAdvertisementsComponent},
  { path: 'advertisement/:id', component: DetailsAdvertisementComponent},
  { path: 'myAds', component: MyAdvertisementsComponent, canActivate: [AuthGuardService]},
  { path: 'advertisements/create', component: CreateAdvertisementComponent, canActivate: [AuthGuardService]},
  { path: 'admin/brandModels', component: ListBrandModelsComponent, canActivate: [AuthGuardService, AdminAuthGuardService]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    scrollPositionRestoration: 'enabled'
  })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
