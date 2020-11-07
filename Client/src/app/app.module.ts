import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthService } from './services/auth/auth.service';
import { HttpClientModule } from '@angular/common/http';
import { UsersModule } from './users/users.module';
import { SharedModule } from './shared/shared.module';
import { AdvertisementsModule } from './advertisements/advertisements.module';
import { AdvertisementService } from './services/advertisement/advertisement.service';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    UsersModule,
    SharedModule,
    AdvertisementsModule
  ],
  providers: [
    AuthService,
    AdvertisementService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
