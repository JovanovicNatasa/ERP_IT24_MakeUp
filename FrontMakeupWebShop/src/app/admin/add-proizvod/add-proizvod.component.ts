import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AddProizvodService } from '../add-proizvod/add-proizvod.service';

interface AddProizvodRequest {
 model: string,
 sastav: string,
 nacinUpotrebe: string,
  kolekcijaId :number;
  cenaPoKom:number;
  brendId:number;
  namenaId:number;
  tipId:number;
  kolicinaNaStanju:number;
}
interface Brend {
  brendId: number;
  nazivBrenda: string;
}
interface Kolekcija {
	kolekcijaId:number;
  nazivKolekcije: string;
}
interface Namena {
  namenaId:number,
  nazivNamene: string;
}
interface Tip {
  tipId:number,
  nazivTipa: string;
}
@Component({
  selector: 'app-add-proizvod',
  templateUrl: './add-proizvod.component.html',
  styleUrls: ['./add-proizvod.component.css'],
   providers: [AddProizvodService]
})
export class AddProizvodComponent implements OnInit {
  addProizvodForm!: FormGroup;
  brendovi: Brend[] = [];
  kolekcije: Kolekcija[] = [];
  tipovi: Tip[] = [];
  namene: Namena[] = [];

  constructor(private formBuilder: FormBuilder, private http: HttpClient) { }

  ngOnInit() {
    this.addProizvodForm = this.formBuilder.group({
  model: ['', Validators.required],
 sastav: ['', Validators.required],
 nacinUpotrebe: ['', Validators.required],
  kolekcijaId :['', Validators.required],
  cenaPoKom:['', Validators.required],
  brendId:['', Validators.required],
  namenaId:['', Validators.required],
  tipId:['', Validators.required],
  kolicinaNaStanju:['', Validators.required]
    });
    this.fetchBrend();
    this.fetchNamena();
    this.fetchKolekcija();
    this.fetchTip();

  }
  get f() {
    return this.addProizvodForm.controls;
  }
  fetchBrend() {
    // Simulated API call to fetch brendovi data
    this.http.get<Brend[]>('https://localhost:44307/Brend')
      .subscribe(
        (response) => {
          this.brendovi = response;
        },
        (error) => {
          console.error('Failed to fetch brendovi', error);
        }
      );
  }
  fetchKolekcija() {
    // Simulated API call to fetch kolekcije data
    this.http
      .get<Kolekcija[]>('https://localhost:44307/Kolekcija')
      .subscribe(
        (response) => {
          this.kolekcije = response;
        },
        (error) => {
          console.error('Failed to fetch kolekcije', error);
        }
      );
  }

  fetchTip() {
    // Simulated API call to fetch tipovi data
    this.http
      .get<Tip[]>('https://localhost:44307/Tip')
      .subscribe(
        (response) => {
          this.tipovi = response;
        },
        (error) => {
          console.error('Failed to fetch tipovi', error);
        }
      );
  }

  fetchNamena() {
    // Simulated API call to fetch namene data
    this.http
      .get<Namena[]>('https://localhost:44307/api/Namena')
      .subscribe(
        (response) => {
          this.namene = response;
        },
        (error) => {
          console.error('Failed to fetch namene', error);
        }
      );
  }
  addProizvod() {
    if (this.addProizvodForm.invalid) {
      console.error('Please fill in all the required fields');
      return;
    }

    const addProizvodRequest: AddProizvodRequest = {
      kolekcijaId: this.addProizvodForm.value.kolekcijaId,
      cenaPoKom: this.addProizvodForm.value.cenaPoKom,
      brendId: this.addProizvodForm.value.brendId,
      namenaId: this.addProizvodForm.value.namenaId,
      tipId: this.addProizvodForm.value.tipId,
      kolicinaNaStanju: this.addProizvodForm.value.kolicinaNaStanju,
	  model: this.addProizvodForm.value.model,
      sastav: this.addProizvodForm.value.sastav,
      nacinUpotrebe: this.addProizvodForm.value.nacinUpotrebe,
    };
    this.http
      .post('https://localhost:44307/Proizvod', addProizvodRequest)
      .subscribe(
        response => {
          console.log('addProizvodRequest successful', response);
          // Perform any additional actions or show success message
        },
        error => {
          console.error('addProizvodRequest failed', error);
          // Handle the error condition, show error message, etc.
        }
      );

  }
}
