import { TestBed } from '@angular/core/testing';

import { CastvoteService } from './castvote.service';

describe('CastvoteService', () => {
  let service: CastvoteService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CastvoteService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
