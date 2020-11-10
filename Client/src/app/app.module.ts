import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthService } from './services/auth/auth.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { UsersModule } from './users/users.module';
import { SharedModule } from './shared/shared.module';
import { AdvertisementsModule } from './advertisements/advertisements.module';
import { AdvertisementService } from './services/advertisement/advertisement.service';
import { BrandModelService } from './services/brandModel/brand-model.service';
import { TokenInterceptorService } from './services/token/token-interceptor.service';
import { AdminModule } from './admin/admin.module';
import { AuthGuardService } from './services/auth/auth-guard.service';
import { AdminAuthGuardService } from './services/auth/admin-auth-guard.service';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    SharedModule,
    AdminModule,
    UsersModule,
    AdvertisementsModule,
  ],
  providers: [
    AuthService,
    AdvertisementService,
    BrandModelService,
    AuthGuardService,
    AdminAuthGuardService,

    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptorService,
      multi: true,
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
