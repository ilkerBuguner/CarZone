import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailsAdvertisementComponent } from './details-advertisement.component';

describe('DetailsAdvertisementComponent', () => {
  let component: DetailsAdvertisementComponent;
  let fixture: ComponentFixture<DetailsAdvertisementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DetailsAdvertisementComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DetailsAdvertisementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
