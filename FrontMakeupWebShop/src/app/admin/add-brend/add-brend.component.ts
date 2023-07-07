import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AdminService } from '../admin.service';
import { AddBrendRequest } from 'src/app/models/api-models/brand.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';



@Component({
  selector: 'app-add-brend',
  templateUrl: './add-brend.component.html',
  styleUrls: ['./add-brend.component.css']
})
export class AddBrendComponent implements OnInit {
  addBrandForm!: FormGroup;

  constructor(private formBuilder: FormBuilder, private http: HttpClient, private adminService:AdminService, private snackBar:MatSnackBar,private router:Router) { }

  ngOnInit() {
    this.addBrandForm = this.formBuilder.group({
      nazivBrenda: ['', Validators.required],
    });
  }

  addBrend() {
    if (this.addBrandForm.invalid) {
      return;
    }

    const addBrendRequest: AddBrendRequest = {
      nazivBrenda: this.addBrandForm.value.nazivBrenda
    };

    this.adminService.addBrend(addBrendRequest)
      .subscribe(
        (response) => {
          console.log('addBrendRequest successful', response);
          // Perform any additional actions or show success message
          this.snackBar.open('Brend added successfully!', undefined, {
            duration:2000
          });
          setTimeout(()=>{

            this.router.navigateByUrl('Pregled-brenda');

          },2000)
        },
        (error) => {
          console.error('addBrendRequest failed', error);
          // Handle the error condition, show error message, etc.
        }
      );
      /*this.adminService.addBrend(addBrendRequest);*/

  }

}
