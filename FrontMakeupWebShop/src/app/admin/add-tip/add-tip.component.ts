import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

interface AddTipRequest {
  nazivTipa: string;
}

@Component({
  selector: 'app-add-tip',
  templateUrl: './add-tip.component.html',
  styleUrls: ['./add-tip.component.css']
})
export class AddTipComponent implements OnInit {
  addTypeForm!: FormGroup;

  constructor(private formBuilder: FormBuilder, private http: HttpClient) { }

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

    this.http.post('https://localhost:44307/Tip', addTipRequest)
      .subscribe(
        (response) => {
          console.log('addTipRequest successful', response);
          // Perform any additional actions or show success message
        },
        (error) => {
          console.error('addTipRequest failed', error);
          // Handle the error condition, show error message, etc.
        }
      );
  }

}
