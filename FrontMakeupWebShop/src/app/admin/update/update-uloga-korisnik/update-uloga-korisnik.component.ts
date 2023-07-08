import { Component, OnInit } from '@angular/core';
import { Uloga, UpdateKorisnikUlogaRequest, User } from 'src/app/models/ui-models/user.model';
import { AdminService } from '../../admin.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-update-uloga-korisnik',
  templateUrl: './update-uloga-korisnik.component.html',
  styleUrls: ['./update-uloga-korisnik.component.css']
})
export class UpdateUlogaKorisnikComponent implements OnInit {
  userId: number | undefined;
  user: User = new User();
  ulogaList: Uloga[] = [];
  updateKorisnikUlogaRequest: UpdateKorisnikUlogaRequest = new UpdateKorisnikUlogaRequest();

  constructor(
    private readonly adminService: AdminService,
    private readonly snackBar: MatSnackBar,
    private readonly route: ActivatedRoute,
    private readonly router:Router
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const idParam = params.get('id');
      this.userId = idParam ? Number(idParam) : undefined;

      if (this.userId) {
        this.adminService.getSingleKorisnik(this.userId).subscribe(
          (successResponse) => {
            this.user = successResponse;
            this.updateKorisnikUlogaRequest.ulogaId = this.user.uloga.ulogaId; // Set the ulogaId in the updateKorisnikUlogaRequest object
          }
        );
        this.adminService.getUloga().subscribe(
          (successResponse) => {
            this.ulogaList = successResponse;
          }
        );
      }
    });
  }

  onUpdate(): void {
    this.updateKorisnikUlogaRequest.ulogaId = this.user.uloga.ulogaId; // Update the ulogaId in the updateKorisnikUlogaRequest object
    this.adminService.updateUlogaKorisnik(this.user.korisnikId, this.updateKorisnikUlogaRequest).subscribe(
      (successResponse) => {
        this.snackBar.open('User updated successfully!', undefined, {
          duration: 2000
        });
      },
      (errorResponse) => {
        this.snackBar.open('Update failed', undefined, {
          duration: 2000
        });
      }
    );
  }


  onDelete():void{
    this.adminService.deleteUser(this.user.korisnikId)
    .subscribe(
      (successResponse)=>{
        this.snackBar.open('Nalog je obrisan!', undefined, {
          duration:2000
        });
        setTimeout(()=>{
          this.router.navigateByUrl('Pregled-korisnika');

        },2000)

      },
      (errorResponse)=>{

        this.snackBar.open('Brisanje neuspešno, korisnik već ima kupovine na svom nalogu', undefined, {
          duration:2000
        });
      }
    )
  }
}
