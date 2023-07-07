import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateBrendComponent } from './update-brend.component';

describe('UpdateBrendComponent', () => {
  let component: UpdateBrendComponent;
  let fixture: ComponentFixture<UpdateBrendComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdateBrendComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdateBrendComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
