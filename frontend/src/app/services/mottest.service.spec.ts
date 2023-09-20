import { TestBed } from '@angular/core/testing';

import { MottestService } from './mottest.service';

describe('MottestService', () => {
  let service: MottestService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MottestService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
