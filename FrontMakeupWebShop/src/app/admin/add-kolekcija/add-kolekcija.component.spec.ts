import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddKolekcijaComponent } from './add-kolekcija.component';

describe('AddKolekcijaComponent', () => {
  let component: AddKolekcijaComponent;
  let fixture: ComponentFixture<AddKolekcijaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddKolekcijaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddKolekcijaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
