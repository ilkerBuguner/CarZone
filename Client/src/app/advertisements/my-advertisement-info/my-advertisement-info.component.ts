import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { IAdvertisement } from 'src/app/models/IAdvertisement';

@Component({
  selector: 'app-my-advertisement-info',
  templateUrl: './my-advertisement-info.component.html',
  styleUrls: ['./my-advertisement-info.component.css']
})
export class MyAdvertisementInfoComponent implements OnChanges {
  @Input() advertisement: IAdvertisement;
  selectedAdvertisement: IAdvertisement;
  constructor() { }

  ngOnChanges(changes: SimpleChanges): void {
    this.selectedAdvertisement = changes.advertisement.currentValue;
  }

}
