import { TestBed } from '@angular/core/testing';

import { AddProizvodService } from './add-proizvod.service';

describe('AddProizvodService', () => {
  let service: AddProizvodService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddProizvodService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
