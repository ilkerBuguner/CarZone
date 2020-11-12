import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Advertisement } from '../../models/Advertisement';
import { AdvertisementService } from '../../services/advertisement/advertisement.service';
import { map, mergeMap } from 'rxjs/operators';

@Component({
  selector: 'app-details-advertisement',
  templateUrl: './details-advertisement.component.html',
  styleUrls: ['./details-advertisement.component.css']
})
export class DetailsAdvertisementComponent implements OnInit {
  advertisement: Advertisement;
  id: string;

  constructor(private advertsementService: AdvertisementService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.fetchData();
  }

  fetchData() {
    this.route.params.pipe(map(params => {
      const id = params['id'];
      return id;
    }), mergeMap(id => this.advertsementService.getAdvertisement(id))).subscribe(res => {
      this.advertisement = res;
      console.log(res);
    })
  }

}
