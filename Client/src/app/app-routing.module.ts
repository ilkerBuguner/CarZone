import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ListAdvertisementsComponent } from './advertisements/list-advertisements/list-advertisements.component';
import { DetailsAdvertisementComponent } from './advertisements/details-advertisement/details-advertisement.component';
import { LoginComponent } from './users/login/login.component';
import { RegisterComponent } from './users/register/register.component';

const routes: Routes = [
  { path: '', component: ListAdvertisementsComponent},
  { path: 'login', component: LoginComponent},
  { path: 'register', component: RegisterComponent},
  { path: 'advertisements', component: ListAdvertisementsComponent},
  { path: 'advertisements/:id', component: DetailsAdvertisementComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    scrollPositionRestoration: 'enabled'
  })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
