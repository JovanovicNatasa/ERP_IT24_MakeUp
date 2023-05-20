import { TestBed } from '@angular/core/testing';

import { SerachSService } from './serach-s.service';

describe('SerachSService', () => {
  let service: SerachSService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SerachSService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
