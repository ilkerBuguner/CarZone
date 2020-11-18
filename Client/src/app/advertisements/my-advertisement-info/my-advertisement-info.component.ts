import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { IAdvertisement } from 'src/app/models/IAdvertisement';
import { AdvertisementService } from 'src/app/services/advertisement/advertisement.service';

@Component({
  selector: 'app-my-advertisement-info',
  templateUrl: './my-advertisement-info.component.html',
  styleUrls: ['./my-advertisement-info.component.css']
})
export class MyAdvertisementInfoComponent implements OnChanges {
  @Input() advertisement: IAdvertisement;
  selectedAdvertisement: IAdvertisement;
  constructor(
    private advertisementService: AdvertisementService,
    private toastrService: ToastrService,
    private router: Router) {
      this.router.routeReuseStrategy.shouldReuseRoute = () => false;
      this.router.onSameUrlNavigation = 'reload';
    }

  ngOnChanges(changes: SimpleChanges): void {
    this.selectedAdvertisement = changes.advertisement.currentValue;
  }

  deleteAdvertisement() {
    this.advertisementService.delete(this.selectedAdvertisement.id).subscribe(res => {
      this.toastrService.success('Successfully deleted advertisement!');
      const closeButton = document.querySelector(".close-button") as HTMLElement;
      closeButton.click();
      this.router.navigate(['myAds'])
    })
  }

}
