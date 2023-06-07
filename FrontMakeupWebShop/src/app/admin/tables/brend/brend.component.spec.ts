import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BrendComponent } from './brend.component';

describe('BrendComponent', () => {
  let component: BrendComponent;
  let fixture: ComponentFixture<BrendComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BrendComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BrendComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
