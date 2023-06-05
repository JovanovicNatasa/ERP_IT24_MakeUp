import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

interface AddBrendRequest {
  nazivBrenda: string;
}

@Component({
  selector: 'app-add-brend',
  templateUrl: './add-brend.component.html',
  styleUrls: ['./add-brend.component.css']
})
export class AddBrendComponent implements OnInit {
  addBrandForm!: FormGroup;

  constructor(private formBuilder: FormBuilder, private http: HttpClient) { }

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

    this.http.post('https://localhost:44307/Brend', addBrendRequest)
      .subscribe(
        (response) => {
          console.log('addBrendRequest successful', response);
          // Perform any additional actions or show success message
        },
        (error) => {
          console.error('addBrendRequest failed', error);
          // Handle the error condition, show error message, etc.
        }
      );
  }

}
