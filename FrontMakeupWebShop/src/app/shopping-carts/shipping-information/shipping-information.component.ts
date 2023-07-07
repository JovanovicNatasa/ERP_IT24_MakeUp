import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { LoginService } from 'src/app/users/login/login.service';
import { ProfileService } from 'src/app/users/profile/profile.service';
import { User } from 'src/app/models/api-models/user.model';
import { UpdateUser } from 'src/app/models/api-models/updateUser.model';
import { MatDialog } from '@angular/material/dialog';
import { SuccessDialogComponent } from 'src/app/dialog/success-dialog/success-dialog.component';
import { ShoppingCartsService } from '../shopping-carts.service';


@Component({
  selector: 'app-shipping-information',
  templateUrl: './shipping-information.component.html',
  styleUrls: ['./shipping-information.component.css']
})
export class ShippingInformationComponent implements OnInit {

  email:string='';

  shippingInfoForm: FormGroup = new FormGroup({}); // Initialize with an empty FormGroup

  constructor(private loginService: LoginService, private profileService: ProfileService, private dialog: MatDialog,private shoppingService:ShoppingCartsService) { }


  getUsername(): string {
    return this.loginService.getUsername();
  }

  getPassword():string{
    return this.loginService.getPassword();
  }
  getKorisnikId():number{
    return this.loginService.getKorisnikId();
  }

  ngOnInit() {
    this.profileService.getKorisnikByUsername(this.getUsername()).subscribe((userData: User) => {
      this.initializeForm(userData); // Initialize the form after receiving user data
      this.email=userData.email;
    });
  }

  initializeForm(userData: User) {
    this.shippingInfoForm = new FormGroup({
      ime: new FormControl(userData.ime, Validators.required),
      prezime: new FormControl(userData.prezime, Validators.required),
      kontakt: new FormControl(userData.kontakt, Validators.required),
      grad: new FormControl(userData.adresa.grad, Validators.required),
      ulica: new FormControl(userData.adresa.ulica, Validators.required),
      broj: new FormControl(userData.adresa.broj, Validators.required),
      postanskiBroj: new FormControl(userData.adresa.postanskiBroj, Validators.required)
    });
  }

  updateUser() {
    // Check if the form is valid
    if (this.shippingInfoForm.invalid) {
      console.error('Please fill in all the required fields');
      return;
    }

    const updatedUser: UpdateUser = {
      ime: this.shippingInfoForm.value.ime,
      prezime: this.shippingInfoForm.value.prezime,
      kontakt: this.shippingInfoForm.value.kontakt,
      email: this.email,
      username: this.getUsername(),
      lozinka: this.getPassword(),
      adresa: {
        adresaId: 0,
        grad: this.shippingInfoForm.value.grad,
        ulica: this.shippingInfoForm.value.ulica,
        broj: this.shippingInfoForm.value.broj,
        postanskiBroj: this.shippingInfoForm.value.postanskiBroj
      },

    };

    this.profileService.updateUser(this.getKorisnikId(), updatedUser).subscribe(
      (response: any) => {
        console.log('User updated successfully:', response);
        this.openSuccessDialog(); // Call the method to open the success dialog
      },
      (error: any) => {
        console.error('User update failed:', error);
        // Optionally display an error message to the user
      }
    );
  }

  openSuccessDialog() {
    const dialogRef = this.dialog.open(SuccessDialogComponent, {
      width: '250px',
      data: 'Podaci o korisniku su uspešno izmenjeni' // Pass the message to the dialog component
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      // Perform any additional actions after the dialog is closed (if needed)
    });
  }

  createPaymentIntent() {

    const racunId=this.shoppingService.getRacunId();

    try {
      // Call the API to create the "racun"
      this.shoppingService.createPaymentIntent(racunId).subscribe(
        (response: any) => {
          // Log the response to the console
          console.log("PaymentIntent created:", response);
        },
        (error: any) => {
          // Handle error while creating "PaymentIntent"
          console.error("Error creating PaymentIntent:", error);
        }
      );
    } catch (error) {
      // Handle error while creating "PaymentIntent"
      console.error("Error creating PaymentIntent:", error);
    }
  }

}
