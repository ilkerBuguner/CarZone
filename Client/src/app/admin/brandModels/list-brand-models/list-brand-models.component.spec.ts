import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListBrandModelsComponent } from './list-brand-models.component';

describe('ListBrandModelsComponent', () => {
  let component: ListBrandModelsComponent;
  let fixture: ComponentFixture<ListBrandModelsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListBrandModelsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ListBrandModelsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
