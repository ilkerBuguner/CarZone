import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Advertisement } from '../../models/Advertisement';
import { AdvertisementService } from '../../services/advertisement/advertisement.service';

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
    this.route.params.subscribe(res => {
      this.id = res['id'];
      this.advertsementService.getAdvertisement(this.id).subscribe(data => {
        this.advertisement = data;
        console.log(data);
      })
    })
  }

  images = [944, 1011, 984].map((n) => `https://picsum.photos/id/${n}/900/500`);

}
