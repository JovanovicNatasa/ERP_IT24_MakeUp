import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AdminService } from '../admin.service';
import { AddNamenaRequest } from 'src/app/models/api-models/purpose.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-namena',
  templateUrl: './add-namena.component.html',
  styleUrls: ['./add-namena.component.css']
})
export class AddNamenaComponent implements OnInit {
  addPurposeForm!: FormGroup;

  constructor(private formBuilder: FormBuilder, private http: HttpClient,private adminService:AdminService,private snackBar:MatSnackBar,private router: Router) { }

  ngOnInit() {
    this.addPurposeForm = this.formBuilder.group({
      nazivNamene: ['', Validators.required],
    });
  }

  addNamena() {
    if (this.addPurposeForm.invalid) {
      return;
    }

    const addNamenaRequest: AddNamenaRequest = {
      nazivNamene: this.addPurposeForm.value.nazivNamene
    };

    this.adminService.addNamena(addNamenaRequest)
    .subscribe(
      (response) => {
        console.log('addNamenaRequest successful', response);
        // Perform any additional actions or show success message
        this.snackBar.open('Namena added successfully!', undefined, {
          duration:2000
        });
        setTimeout(()=>{

          this.router.navigateByUrl('Pregled-namene');

        },2000)
      },
      (error) => {
        console.error('addNamenaRequest failed', error);
        // Handle the error condition, show error message, etc.
      }
    );
  }

}

