import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateUlogaKorisnikComponent } from './update-uloga-korisnik.component';

describe('UpdateUlogaKorisnikComponent', () => {
  let component: UpdateUlogaKorisnikComponent;
  let fixture: ComponentFixture<UpdateUlogaKorisnikComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdateUlogaKorisnikComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdateUlogaKorisnikComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
