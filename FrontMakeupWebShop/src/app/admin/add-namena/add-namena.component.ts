import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

interface AddNamenaRequest {
  nazivNamene: string;
}

@Component({
  selector: 'app-add-namena',
  templateUrl: './add-namena.component.html',
  styleUrls: ['./add-namena.component.css']
})
export class AddNamenaComponent implements OnInit {
  addPurposeForm!: FormGroup;

  constructor(private formBuilder: FormBuilder, private http: HttpClient) { }

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

    this.http.post('https://localhost:44307/api/Namena', addNamenaRequest)
      .subscribe(
        (response) => {
          console.log('addNamenaRequest successful', response);
          // Perform any additional actions or show success message
        },
        (error) => {
          console.error('addNamenaRequest failed', error);
          // Handle the error condition, show error message, etc.
        }
      );
  }

}

