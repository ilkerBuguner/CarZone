import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateBrandModelComponent } from './create-brand-model.component';

describe('CreateBrandModelComponent', () => {
  let component: CreateBrandModelComponent;
  let fixture: ComponentFixture<CreateBrandModelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateBrandModelComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateBrandModelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
