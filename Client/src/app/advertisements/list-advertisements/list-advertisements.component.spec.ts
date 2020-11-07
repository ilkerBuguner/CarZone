import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListAdvertisementsComponent } from './list-advertisements.component';

describe('ListAdvertisementsComponent', () => {
  let component: ListAdvertisementsComponent;
  let fixture: ComponentFixture<ListAdvertisementsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListAdvertisementsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ListAdvertisementsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
