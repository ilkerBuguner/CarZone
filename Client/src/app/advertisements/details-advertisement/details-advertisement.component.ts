import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IAdvertisement } from '../../models/IAdvertisement';
import { AdvertisementService } from '../../services/advertisement/advertisement.service';
import { map, mergeMap } from 'rxjs/operators';
import { AuthService } from 'src/app/services/auth/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-details-advertisement',
  templateUrl: './details-advertisement.component.html',
  styleUrls: ['./details-advertisement.component.css']
})
export class DetailsAdvertisementComponent implements OnInit {
  advertisement: IAdvertisement;
  id: string;

  get isAdmin() {
    return this.authService.isAdmin();
  }

  get currentUserId() {
    return this.authService.getUserId();
  }

  constructor(
    private advertisementService: AdvertisementService,
     private route: ActivatedRoute,
     private authService: AuthService,
     private toastrService: ToastrService,
     private router: Router) { }

  ngOnInit(): void {
    this.fetchData();
    this.incrementViews();
  }

  fetchData() {
    this.route.params.pipe(map(params => {
      const id = params['id'];
      return id;
    }), mergeMap(id => this.advertisementService.getAdvertisement(id))).subscribe(res => {
      this.advertisement = res;
    })
  }

  incrementViews() {
    this.route.params.pipe(map(params => {
      const id = params['id'];
      return id;
    }), mergeMap(id => this.advertisementService.incrementViews(id))).subscribe();
  }

  deleteAdvertisement() {
    this.advertisementService.delete(this.advertisement.id).subscribe(res => {
      this.toastrService.success('Successfully deleted advertisement!');
      const closeButton = document.querySelector(".close-button") as HTMLElement;
      closeButton.click();
      this.router.navigate(['advertisements'])
    })
  }

}
