import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AdminService } from '../admin.service';
import { AddTipRequest } from 'src/app/models/api-models/type.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-tip',
  templateUrl: './add-tip.component.html',
  styleUrls: ['./add-tip.component.css']
})
export class AddTipComponent implements OnInit {
  addTypeForm!: FormGroup;

  constructor(private formBuilder: FormBuilder, private http: HttpClient,private adminService:AdminService, private snackBar: MatSnackBar, private router : Router) { }

  ngOnInit() {
    this.addTypeForm = this.formBuilder.group({
      nazivTipa: ['', Validators.required],
    });
  }

  addTip() {
    if (this.addTypeForm.invalid) {
      return;
    }

    const addTipRequest: AddTipRequest = {
      nazivTipa: this.addTypeForm.value.nazivTipa
    };

    this.adminService.addTip(addTipRequest)
    .subscribe(
      (response) => {
        console.log('addTipRequest successful', response);
        // Perform any additional actions or show success message
        this.snackBar.open('Tip added successfully!', undefined, {
          duration:2000
        });
        setTimeout(()=>{

          this.router.navigateByUrl('Pregled-tipa');

        },2000)
      },
      (error) => {
        console.error('addTipRequest failed', error);
        // Handle the error condition, show error message, etc.
      }
    );
  }

}
