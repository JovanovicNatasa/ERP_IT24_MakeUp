import { Component, OnInit } from '@angular/core';
import { LoginService } from './login.service';
import { Login } from 'src/app/models/ui-models/login.model';
import { threadId } from 'worker_threads';
import { waitForAsync } from '@angular/core/testing';
import { ShoppingCart } from 'src/app/models/ui-models/shopping-basket.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  token: string = '';
  korpaRequest=new ShoppingCart();
  loginForm!: FormGroup;


  constructor(private loginService: LoginService,private formBuilder: FormBuilder) {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      lozinka: ['', Validators.required]
    });

  }

  ngOnInit(): void {
  }

  login(): void {
    if (this.loginForm.invalid) {
      console.error('Please fill in all the required fields');
      return;
    }

    const username = this.loginForm.value.username;
    const lozinka = this.loginForm.value.lozinka;

    const loginRequest = {
      username: username,
      lozinka: lozinka
    };

    this.loginService.loginUser(loginRequest).subscribe(
      (response: string) => {
        // Store the token in local storage and set it in the LoginService
        this.loginService.setToken(response);

        // Display success message along with the token
        this.token = 'success! ' + response;

        // Log the response to the console
        console.log('Response:', response);

        this.loginService.setUsername(username);

        // Call createKorpa() after successful login
        this.createKorpa();
      },
      (error: any) => {
        // Handle login error
        console.error('Error:', error);
      }
    );
  }

  logout() {
    console.log('Logout button clicked');
    this.loginService.logout(); // Call the logout() method from the LoginServiceService
    this.token = ''; // Clear the token variable in the component
  }


  createKorpa(): void {
    // Get the korisnikId from the LoginService
    const korisnikId = this.loginService.getKorisnikId();

    // Get the korpaId from the LoginService
    const korpaId = this.loginService.getKorpaId();

    this.korpaRequest.korisnikId = korisnikId;
    this.korpaRequest.korpaId = korpaId;

    console.log('User ID:', korisnikId);

    try {
      // Call the API to create the "korpa"
      this.loginService.createKorpa().subscribe(
        (response: any) => {
          // Log the response to the console
          console.log("Korpa created:", response);
        },
        (error: any) => {
          // Handle error while creating "korpa"
          console.error("Error creating korpa:", error);
        }
      );
    } catch (error) {
      // Handle error while creating "korpa"
      console.error("Error creating korpa:", error);
    }
  }


  MarkAllAsTouched(){
    Object.values(this.loginForm.controls).forEach(control => {
      control.markAsTouched();
    });
  }
}
