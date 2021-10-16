import { TestBed } from '@angular/core/testing';

import { ActivatedLogin } from './activated-login.service';

describe('ActivatedLoginService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ActivatedLogin = TestBed.get(ActivatedLogin);
    expect(service).toBeTruthy();
  });
});
