import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { LoginService } from '../login/login.service';
import { ProfileService } from './profile.service';
import { MatDialog } from '@angular/material/dialog';
import { AdminService } from 'src/app/admin/admin.service';
import { User } from 'src/app/models/api-models/user.model';
import { UpdateUser } from 'src/app/models/api-models/updateUser.model';
import { SuccessDialogComponent } from 'src/app/dialog/success-dialog/success-dialog.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  email:string='';
  ulogaId:number=0;
  brojKupovina:number=0;

  profileForm: FormGroup = new FormGroup({}); // Initialize with an empty FormGroup
  isEditMode: boolean = false;

  constructor(private loginService: LoginService, private profileService: ProfileService, private dialog: MatDialog,private adminService:AdminService,private readonly snackBar:MatSnackBar, private router:Router) { }


  getKorisnikId():number{
    return this.loginService.getKorisnikId();
  }
  getUsername(): string {
    return this.loginService.getUsername();
  }
  getKorpaId():number{
    return this.loginService.getKorpaId();
  }
  getBrojKupovina():number{
    return this.brojKupovina
  }

  ngOnInit() {
    this.profileService.getKorisnikByUsername(this.getUsername()).subscribe((userData: User) => {
      this.initializeForm(userData); // Initialize the form after receiving user data
      this.email=userData.email;
      this.ulogaId=userData.ulogaId;
      this.brojKupovina=userData.brojKupovina;
    });
  }

  initializeForm(userData: User) {
    this.profileForm = new FormGroup({
      ime: new FormControl(userData.ime, Validators.required),
      prezime: new FormControl(userData.prezime, Validators.required),
      kontakt: new FormControl(userData.kontakt, Validators.required),
	  username: new FormControl(userData.username, Validators.required),
      lozinka: new FormControl(userData.lozinka, Validators.required),
      grad: new FormControl(userData.adresa.grad, Validators.required),
      ulica: new FormControl(userData.adresa.ulica, Validators.required),
      broj: new FormControl(userData.adresa.broj, Validators.required),
      postanskiBroj: new FormControl(userData.adresa.postanskiBroj, Validators.required)
    });
  }

  toggleEditMode(): void {
    this.isEditMode = !this.isEditMode;
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

  updateUser() {
    // Check if the form is valid
    if (this.profileForm.invalid) {
      console.error('Please fill in all the required fields');
      return;
    }

    const updatedUser: UpdateUser = {
      ime: this.profileForm.value.ime,
      prezime: this.profileForm.value.prezime,
      kontakt: this.profileForm.value.kontakt,
      email: this.email,
      username: this.profileForm.value.username,
      lozinka: this.profileForm.value.lozinka,
      adresa: {
        adresaId: 0,
        grad: this.profileForm.value.grad,
        ulica: this.profileForm.value.ulica,
        broj: this.profileForm.value.broj,
        postanskiBroj: this.profileForm.value.postanskiBroj
      },

    };

    this.profileService.updateUser(this.getKorisnikId(), updatedUser).subscribe(
      (response: any) => {
        console.log('User updated successfully:', response);
        this.openSuccessDialog(); // Call the method to open the success dialog
        this.isEditMode = false;
      },
      (error: any) => {
        console.error('User update failed:', error);
        // Optionally display an error message to the user
      }
    );
  }


  logout() {
    if(this.ulogaId==1){
      this.deleteKorpa();
    }
    console.log('Korisnik izlogovan');
    this.loginService.logout(); // Call the logout() method from the LoginServiceService
    this.loginService.setLogOut(true);
  }



  deleteKorpa():void{
      // Call the API to create the "korpa"
      this.loginService.deleteKorpa(this.getKorpaId()).subscribe(
        (response: any) => {
          // Log the response to the console
          console.log("Korpa deleted:", response);
        },
        (error: any) => {
          // Handle error while creating "korpa"
          console.error("Error deliting korpa:", error);
        }
      );
  }


}
