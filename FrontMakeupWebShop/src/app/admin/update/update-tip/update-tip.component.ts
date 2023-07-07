import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../admin.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Type } from 'src/app/models/ui-models/type.model';

@Component({
  selector: 'app-update-tip',
  templateUrl: './update-tip.component.html',
  styleUrls: ['./update-tip.component.css']
})
export class UpdateTipComponent implements OnInit {
  tipId: number | undefined;
  tip:Type={
    tipId: 0 ,
	  nazivTipa: ''
  }

  constructor(private readonly adminService: AdminService, private readonly route: ActivatedRoute, private snackBar: MatSnackBar, private router:Router) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const idParam = params.get('id');
      this.tipId = idParam ? Number(idParam) : undefined;

      if (this.tipId) {
        this.adminService.getSingleTip(this.tipId)
        .subscribe(
          (successResponse)=>{
            this.tip= successResponse;
          }
        );
      }
    });
  }
  onUpdate():void{


    this.adminService.updateTip(this.tip, this.tip.tipId)
    .subscribe(
      (successResponse)=>{
        this.snackBar.open('Tip updated successfully!', undefined, {
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

    this.adminService.deleteTip(this.tip.tipId)
    .subscribe(
      (successResponse)=>{
        this.snackBar.open('Tip deleted successfully!', undefined, {
          duration:2000
        });
        setTimeout(()=>{

          this.router.navigateByUrl('Pregled-tipa');

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
