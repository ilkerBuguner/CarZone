import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyAdvertisementInfoComponent } from './my-advertisement-info.component';

describe('MyAdvertisementInfoComponent', () => {
  let component: MyAdvertisementInfoComponent;
  let fixture: ComponentFixture<MyAdvertisementInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MyAdvertisementInfoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MyAdvertisementInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
