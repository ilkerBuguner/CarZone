import { TestBed } from '@angular/core/testing';

import { BrandModelService } from './brand-model.service';

describe('BrandModelService', () => {
  let service: BrandModelService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BrandModelService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
