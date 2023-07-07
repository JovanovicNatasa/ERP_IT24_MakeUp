import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateNamenaComponent } from './update-namena.component';

describe('UpdateNamenaComponent', () => {
  let component: UpdateNamenaComponent;
  let fixture: ComponentFixture<UpdateNamenaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdateNamenaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdateNamenaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
