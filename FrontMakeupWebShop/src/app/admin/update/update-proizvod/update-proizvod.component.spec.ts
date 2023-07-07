import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateProizvodComponent } from './update-proizvod.component';

describe('UpdateProizvodComponent', () => {
  let component: UpdateProizvodComponent;
  let fixture: ComponentFixture<UpdateProizvodComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdateProizvodComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdateProizvodComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
