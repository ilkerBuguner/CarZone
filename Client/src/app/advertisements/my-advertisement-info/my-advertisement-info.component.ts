import { Component, Input, OnInit } from '@angular/core';
import { IAdvertisement } from 'src/app/models/IAdvertisement';

@Component({
  selector: 'app-my-advertisement-info',
  templateUrl: './my-advertisement-info.component.html',
  styleUrls: ['./my-advertisement-info.component.css']
})
export class MyAdvertisementInfoComponent implements OnInit {
  @Input() advertisement: IAdvertisement;
  constructor() { }

  ngOnInit(): void {
  }

}
