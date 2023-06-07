import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KolekcijaComponent } from './kolekcija.component';

describe('KolekcijaComponent', () => {
  let component: KolekcijaComponent;
  let fixture: ComponentFixture<KolekcijaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ KolekcijaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(KolekcijaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
