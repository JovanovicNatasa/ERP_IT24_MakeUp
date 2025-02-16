import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AdminService } from '../admin.service';
import { AddProizvodRequest } from 'src/app/models/api-models/product.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
//import { saveAs } from '@progress/kendo-file-saver';



interface Brend {
  brendId: number;
  nazivBrenda: string;
}

interface Kolekcija {
  kolekcijaId: number;
  nazivKolekcije: string;
}

interface Namena {
  namenaId: number,
  nazivNamene: string;
}

interface Tip {
  tipId: number,
  nazivTipa: string;
}

@Component({
  selector: 'app-add-proizvod',
  templateUrl: './add-proizvod.component.html',
  styleUrls: ['./add-proizvod.component.css'],
})
export class AddProizvodComponent implements OnInit {
  addProizvodForm!: FormGroup;
  brendovi: Brend[] = [];
  kolekcije: Kolekcija[] = [];
  tipovi: Tip[] = [];
  namene: Namena[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private http: HttpClient,
    private adminService: AdminService,
    private snackBar: MatSnackBar,
    private router: Router
  ) {}

  ngOnInit() {
    this.addProizvodForm = this.formBuilder.group({
      model: ['', Validators.required],
      sastav: ['', Validators.required],
      nacinUpotrebe: ['', Validators.required],
      kolekcijaId: ['', Validators.required],
      cenaPoKom: ['', Validators.required],
      brendId: ['', Validators.required],
      namenaId: ['', Validators.required],
      tipId: ['', Validators.required],
      kolicinaNaStanju: ['', Validators.required],
      //image: ['', Validators.required]
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
    this.http.get<Brend[]>('https://localhost:44307/Brend').subscribe(
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
    this.http.get<Kolekcija[]>('https://localhost:44307/Kolekcija').subscribe(
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
    this.http.get<Tip[]>('https://localhost:44307/Tip').subscribe(
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
    this.http.get<Namena[]>('https://localhost:44307/api/Namena').subscribe(
      (response) => {
        this.namene = response;
      },
      (error) => {
        console.error('Failed to fetch namene', error);
      }
    );
  }

  onImageSelected(event: any) {
    const file: File = event.target.files[0];
    this.addProizvodForm.patchValue({ image: file });
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

    this.adminService.addProizvod(addProizvodRequest)
    .subscribe(
      (response) => {
        console.log('addProizvodRequest successful', response);
        // Perform any additional actions or show success message
        this.snackBar.open('Proizvod added successfully!', undefined, {
          duration:2000
        });
        setTimeout(()=>{

          this.router.navigateByUrl('Pregled-proizvoda');

        },2000)
      },
      (error) => {
        console.error('addBrendRequest failed', error);
        // Handle the error condition, show error message, etc.
      }
    );

  }
}

  /*addProizvod() {
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






    

    this.adminService.addProizvod(addProizvodRequest).subscribe(
      (response) => {
        console.log('addProizvodRequest successful', response);
        // Perform any additional actions or show success message
        this.snackBar.open('Proizvod added successfully!', undefined, {
          duration: 2000,
        });
        setTimeout(() => {
          this.router.navigateByUrl('Pregled-proizvoda');
        }, 2000);

        // Get the proizvodId from the response or fetch it from the backend
        const proizvodId = response.proizvodId; // Preuzmite proizvodId iz odgovora servera

        const file = this.addProizvodForm.get('image')?.value;
        const formData = new FormData();
        formData.append('file', file);
        
        this.adminService.uploadImage(formData, proizvodId).subscribe(
          (result) => {
            console.log('Image uploaded successfully', result);
            // Dodatne akcije ili prikaz poruke o uspehu
          },
          (error) => {
            console.error('Failed to upload image', error);
            // Obrada greške ili prikaz poruke o grešci
          }
        );
        
      },
      (error) => {
        console.error('addProizvodRequest failed', error);
        // Handle the error condition, show error message, etc.
      }
    );
  }
}*/
