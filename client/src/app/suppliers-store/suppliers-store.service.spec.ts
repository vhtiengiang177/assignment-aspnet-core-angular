import { TestBed } from '@angular/core/testing';

import { SuppliersStoreService } from './suppliers-store.service';

describe('SuppliersStoreService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: SuppliersStoreService = TestBed.get(SuppliersStoreService);
    expect(service).toBeTruthy();
  });
});
