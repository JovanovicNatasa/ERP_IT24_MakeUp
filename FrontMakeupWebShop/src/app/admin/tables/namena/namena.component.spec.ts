import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NamenaComponent } from './namena.component';

describe('NamenaComponent', () => {
  let component: NamenaComponent;
  let fixture: ComponentFixture<NamenaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NamenaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NamenaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
