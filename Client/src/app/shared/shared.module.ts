import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { RouterModule } from '@angular/router';
import { NotFoundComponent } from './not-found/not-found.component';
import { LoaderComponent } from './loader/loader.component';
import { ShortenUsernamePipe } from './pipes/shorten-username.pipe';



@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    NotFoundComponent,
    LoaderComponent,
    ShortenUsernamePipe
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports: [
    HeaderComponent,
    FooterComponent,
    LoaderComponent,
    ShortenUsernamePipe
  ]
})
export class SharedModule { }
