import { Component, OnInit } from '@angular/core';
import { IAdvertisement } from 'src/app/models/IAdvertisement';
import { AdvertisementService } from 'src/app/services/advertisement/advertisement.service';

@Component({
  selector: 'app-my-advertisements',
  templateUrl: './my-advertisements.component.html',
  styleUrls: ['./my-advertisements.component.css']
})
export class MyAdvertisementsComponent implements OnInit {
  advertisements: IAdvertisement[];
  selectedAdvertisement: IAdvertisement;

  constructor(
    private advertisementService: AdvertisementService) { }

  ngOnInit(): void {
    this.advertisementService.getAdvertisementsOfUser().subscribe(ads => {
      this.advertisements = ads;
    })
  }

  selectAd(advertisementId: string) {
    this.advertisementService.getAdvertisement(advertisementId).subscribe(ad => {
      this.selectedAdvertisement = ad;
    })
  }

}
