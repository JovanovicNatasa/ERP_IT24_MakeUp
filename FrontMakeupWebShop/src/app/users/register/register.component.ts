import { Component, OnInit } from '@angular/core';
import { RegisterService } from './register.service';
import { User } from 'src/app/models/ui-models/user.model';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;
  registerData = new User();

  constructor(private registerService: RegisterService, private formBuilder: FormBuilder) {}

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      username: ['', Validators.required],
      email: ['', [Validators.required, Validators.email, this.emailFormatValidator]],
      lozinka: ['', Validators.required],
      ime: ["", [Validators.required,]],
      prezime: ['', Validators.required],
      kontakt: ['', Validators.required],
      grad: ['', Validators.required],
      ulica: ['', Validators.required],
      broj: ['', Validators.required],
      postanskiBroj: ['', Validators.required]
    });
  }

  get f() {
    return this.registerForm.controls;
  }

  getErrorMessage() {
    if (this.registerForm.get('email')?.hasError('required')) {
      return 'You must enter a value';
    }

    if (this.registerForm.get('email')?.hasError('email')) {
      return 'Not a valid email';
    }

    return 'Email must contain "@" symbol and ".com" domain';
  }

  emailFormatValidator(control: FormControl) {
    const email = control.value;
    if (email && email.indexOf('@') === -1) {
      return { missingAtSymbol: true };
    }

    if (email && email.indexOf('.com') === -1) {
      return { missingDotCom: true };
    }

    return null;
  }

  register(registerData: User): void {
    if (this.registerForm.invalid) {
      console.error('Please fill in all the required fields');
      return;
    }

    // Assign the form values to the registerData object
    this.registerData.ime = this.registerForm.value.ime;
    this.registerData.prezime = this.registerForm.value.prezime;
    this.registerData.email = this.registerForm.value.email;
    this.registerData.kontakt = this.registerForm.value.kontakt;
    this.registerData.username = this.registerForm.value.username;
    this.registerData.lozinka = this.registerForm.value.lozinka;
    this.registerData.adresa.grad = this.registerForm.value.grad;
    this.registerData.adresa.ulica = this.registerForm.value.ulica;
    this.registerData.adresa.broj = this.registerForm.value.broj;
    this.registerData.adresa.postanskiBroj = this.registerForm.value.postanskiBroj;

    // Assuming that the `registerUser` method of `RegisterService` returns an Observable
    this.registerService.registerUser(this.registerData).subscribe(
      (response: any) => {
        // Handle successful registration
        console.log("Registration successful:", response);
        // Optionally display a success message to the user
      },
      (error: any) => {
        // Handle registration error
        console.error("Registration failed:", error);
        // Log the complete error response
        console.log("Error response:", error.error);
        // Check if the error response contains validation errors for the Adresa field
        if (error.error && error.error.errors && error.error.errors['$.adresa.postanskiBroj']) {
          console.log("Adresa validation errors:", error.error.errors['$.adresa.postanskiBroj']);
          // Optionally display an error message to the user
        }
      }
    );
  }


  markAllAsTouched() {
    Object.values(this.registerForm.controls).forEach((control) => {
      control.markAsTouched();
    });
  }
}
