import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddBrendComponent } from './add-brend.component';

describe('AddBrendComponent', () => {
  let component: AddBrendComponent;
  let fixture: ComponentFixture<AddBrendComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddBrendComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddBrendComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
