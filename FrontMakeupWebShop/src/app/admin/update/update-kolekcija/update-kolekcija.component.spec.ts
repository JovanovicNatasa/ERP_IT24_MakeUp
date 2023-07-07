import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateKolekcijaComponent } from './update-kolekcija.component';

describe('UpdateKolekcijaComponent', () => {
  let component: UpdateKolekcijaComponent;
  let fixture: ComponentFixture<UpdateKolekcijaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdateKolekcijaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdateKolekcijaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
