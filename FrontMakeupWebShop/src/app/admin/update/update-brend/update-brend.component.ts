import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../admin.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Brand } from 'src/app/models/ui-models/brand.model';

@Component({
  selector: 'app-update-brend',
  templateUrl: './update-brend.component.html',
  styleUrls: ['./update-brend.component.css']
})
export class UpdateBrendComponent implements OnInit {
  brendId: number | undefined;
  brend:Brand={
    brendId: 0 ,
	  nazivBrenda: ''
  }

  constructor(private readonly adminService: AdminService, private readonly route: ActivatedRoute, private snackBar: MatSnackBar, private router:Router) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const idParam = params.get('id');
      this.brendId = idParam ? Number(idParam) : undefined;

      if (this.brendId) {
        this.adminService.getSingleBrend(this.brendId)
        .subscribe(
          (successResponse)=>{
            this.brend= successResponse;
          }
        );
      }
    });
  }
  onUpdate():void{


    this.adminService.updateBrend(this.brend, this.brend.brendId)
    .subscribe(
      (successResponse)=>{
        this.snackBar.open('Brend updated successfully!', undefined, {
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

    this.adminService.deleteBrend(this.brend.brendId)
    .subscribe(
      (successResponse)=>{
        this.snackBar.open('Brend deleted successfully!', undefined, {
          duration:2000
        });
        setTimeout(()=>{

          this.router.navigateByUrl('Pregled-brenda');

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
