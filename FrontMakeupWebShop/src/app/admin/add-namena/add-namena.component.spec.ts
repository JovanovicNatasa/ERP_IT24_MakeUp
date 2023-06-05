import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddNamenaComponent } from './add-namena.component';

describe('AddNamenaComponent', () => {
  let component: AddNamenaComponent;
  let fixture: ComponentFixture<AddNamenaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddNamenaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddNamenaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
