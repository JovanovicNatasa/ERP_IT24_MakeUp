import { Component, OnInit } from '@angular/core';
import { Collection } from 'src/app/models/ui-models/collection.model';
import { AdminService } from '../../admin.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-update-kolekcija',
  templateUrl: './update-kolekcija.component.html',
  styleUrls: ['./update-kolekcija.component.css']
})
export class UpdateKolekcijaComponent implements  OnInit {
  kolekcijaId: number | undefined;
  kolekcija:Collection={
    kolekcijaId: 0 ,
	  nazivKolekcije: ''
  }

  constructor(private readonly adminService: AdminService, private readonly route: ActivatedRoute, private snackBar: MatSnackBar, private router:Router) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const idParam = params.get('id');
      this.kolekcijaId = idParam ? Number(idParam) : undefined;

      if (this.kolekcijaId) {
        this.adminService.getSingleKolekcija(this.kolekcijaId)
        .subscribe(
          (successResponse)=>{
            this.kolekcija= successResponse;
          }
        );
      }
    });
  }
  onUpdate():void{


    this.adminService.updateKolekcija(this.kolekcija, this.kolekcija.kolekcijaId)
    .subscribe(
      (successResponse)=>{
        this.snackBar.open('Kolekcija updated successfully!', undefined, {
          duration:2000
        });
      },
      (errorResponse)=>{
        this.snackBar.open('Update failed', undefined, {
          duration:2000
        });

      }
    );



  }
  onDelete():void{

    this.adminService.deleteKolekcija(this.kolekcija.kolekcijaId)
    .subscribe(
      (successResponse)=>{
        this.snackBar.open('Kolekcija deleted successfully!', undefined, {
          duration:2000
        });
        setTimeout(()=>{

          this.router.navigateByUrl('Pregled-kolekcije');

        },2000)

      },
      (errorResponse)=>{

        this.snackBar.open('Update failed', undefined, {
          duration:2000
        });
      }
    )


  }
}
