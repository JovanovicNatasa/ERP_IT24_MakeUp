import { Component, OnInit } from '@angular/core';
import { Purpose } from 'src/app/models/ui-models/purpose.model';
import { AdminService } from '../../admin.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-update-namena',
  templateUrl: './update-namena.component.html',
  styleUrls: ['./update-namena.component.css']
})
export class UpdateNamenaComponent implements OnInit {
  namenaId: number | undefined;
  namena:Purpose={
    namenaId: 0 ,
	  nazivNamene: ''
  }

  constructor(private readonly adminService: AdminService, private readonly route: ActivatedRoute, private snackBar: MatSnackBar, private router:Router) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const idParam = params.get('id');
      this.namenaId = idParam ? Number(idParam) : undefined;

      if (this.namenaId) {
        this.adminService.getSingleNamena(this.namenaId)
        .subscribe(
          (successResponse)=>{
            this.namena= successResponse;
          }
        );
      }
    });
  }
  onUpdate():void{


    this.adminService.updateNamena(this.namena, this.namena.namenaId)
    .subscribe(
      (successResponse)=>{
        this.snackBar.open('Namena updated successfully!', undefined, {
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

    this.adminService.deleteNamena(this.namena.namenaId)
    .subscribe(
      (successResponse)=>{
        this.snackBar.open('Namena deleted successfully!', undefined, {
          duration:2000
        });
        setTimeout(()=>{

          this.router.navigateByUrl('Pregled-namene');

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
