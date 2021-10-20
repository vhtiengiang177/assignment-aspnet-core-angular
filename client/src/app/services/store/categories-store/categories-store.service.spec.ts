import { TestBed } from '@angular/core/testing';

import { CategoriesStoreService } from './categories-store.service';

describe('CategoriesStoreService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CategoriesStoreService = TestBed.get(CategoriesStoreService);
    expect(service).toBeTruthy();
  });
});
