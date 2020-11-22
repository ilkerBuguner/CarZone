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
import { ErrorInterceptorService } from './services/error/error-interceptor.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { CarService } from './services/car/car.service';
import { UploadService } from './services/upload/upload.service';
import { CommentService } from './services/comment/comment.service';
import { ReplyService } from './services/reply/reply.service';

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
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
  ],
  providers: [
    AuthService,
    AdvertisementService,
    BrandModelService,
    CarService,
    CommentService,
    ReplyService,
    UploadService,
    AuthGuardService,
    AdminAuthGuardService,

    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptorService,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptorService,
      multi: true,
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
