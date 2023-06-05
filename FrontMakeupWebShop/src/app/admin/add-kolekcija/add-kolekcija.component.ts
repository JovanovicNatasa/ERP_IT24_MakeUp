import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

interface AddKolekcijaRequest {
  nazivKolekcije: string;
}

@Component({
  selector: 'app-add-kolekcija',
  templateUrl: './add-kolekcija.component.html',
  styleUrls: ['./add-kolekcija.component.css']
})
export class AddKolekcijaComponent implements OnInit {
  addCollectionForm!: FormGroup;

  constructor(private formBuilder: FormBuilder, private http: HttpClient) { }

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

    this.http.post('https://localhost:44307/Kolekcija', addKolekcijaRequest)
      .subscribe(
        (response) => {
          console.log('addKolekcijaRequest successful', response);
          // Perform any additional actions or show success message
        },
        (error) => {
          console.error('addKolekcijaRequest failed', error);
          // Handle the error condition, show error message, etc.
        }
      );
  }

}

