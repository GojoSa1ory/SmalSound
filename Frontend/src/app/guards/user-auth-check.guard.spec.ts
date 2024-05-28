import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { userAuthCheckGuard } from './user-auth-check.guard';

describe('userAuthCheckGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => userAuthCheckGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
