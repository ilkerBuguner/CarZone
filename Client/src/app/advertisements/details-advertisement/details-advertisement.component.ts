import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IAdvertisement } from '../../models/IAdvertisement';
import { AdvertisementService } from '../../services/advertisement/advertisement.service';
import { map, mergeMap } from 'rxjs/operators';

@Component({
  selector: 'app-details-advertisement',
  templateUrl: './details-advertisement.component.html',
  styleUrls: ['./details-advertisement.component.css']
})
export class DetailsAdvertisementComponent implements OnInit {
  advertisement: IAdvertisement;
  id: string;

  constructor(private advertisementService: AdvertisementService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.fetchData();
  }

  fetchData() {
    this.route.params.pipe(map(params => {
      const id = params['id'];
      return id;
    }), mergeMap(id => this.advertisementService.getAdvertisement(id))).subscribe(res => {
      this.advertisement = res;
    })
  }

}
