import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AddKolekcijaRequest } from 'src/app/models/api-models/collection.model';
import { AdminService } from '../admin.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';


@Component({
  selector: 'app-add-kolekcija',
  templateUrl: './add-kolekcija.component.html',
  styleUrls: ['./add-kolekcija.component.css']
})
export class AddKolekcijaComponent implements OnInit {
  addCollectionForm!: FormGroup;

  constructor(private formBuilder: FormBuilder, private http: HttpClient,private adminService:AdminService,private snackBar:MatSnackBar,private router: Router) { }

  ngOnInit() {
    this.addCollectionForm = this.formBuilder.group({
      nazivKolekcije: ['', Validators.required],
    });
  }

  addKolekcija() {
    if (this.addCollectionForm.invalid) {
      return;
    }

    const addKolekcijaRequest: AddKolekcijaRequest = {
      nazivKolekcije: this.addCollectionForm.value.nazivKolekcije
    };

    this.adminService.addKolekcija(addKolekcijaRequest)
    .subscribe(
      (response) => {
        console.log('addKolekcijaRequest successful', response);
        // Perform any additional actions or show success message
        this.snackBar.open('Kolekcija added successfully!', undefined, {
          duration:2000
        });
        setTimeout(()=>{

          this.router.navigateByUrl('Pregled-kolekcije');

        },2000)
      },
      (error) => {
        console.error('addKolekcijaRequest failed', error);
        // Handle the error condition, show error message, etc.
      }
    );

  }

}

