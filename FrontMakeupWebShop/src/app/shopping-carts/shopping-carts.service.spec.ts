import { TestBed } from '@angular/core/testing';

import { ShoppingCartsService } from './shopping-carts.service';

describe('ShoppingCartsService', () => {
  let service: ShoppingCartsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ShoppingCartsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
