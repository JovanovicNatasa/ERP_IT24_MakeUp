import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ShowBill } from 'src/app/models/api-models/show-bill.model';
import { AdminService } from '../../admin.service';

@Component({
  selector: 'app-racun',
  templateUrl: './racun.component.html',
  styleUrls: ['./racun.component.css']
})
export class RacunComponent implements OnInit{

  racun:ShowBill[]=[];

  displayedColumns: string[] = ['racunId', 'datumKupovine', 'vremeKupovine', 'iznosSaPost', 'status'];
  dataSource: MatTableDataSource<ShowBill>= new MatTableDataSource<ShowBill>();

@ViewChild(MatPaginator) matPaginator!:MatPaginator;
@ViewChild(MatSort) matSort!:MatSort;



  constructor(private adminService: AdminService){}

  ngOnInit(): void {

    this.adminService.getRacun()
    .subscribe(
      (successResponse)=>{
        this.racun=successResponse;
        this.dataSource=new MatTableDataSource<ShowBill>(this.racun);

        if(this.matPaginator){
          this.dataSource.paginator=this.matPaginator;
        }
        if(this.matSort){
          this.dataSource.sort=this.matSort;
        }
      },
      (errorResponse)=>{
        console.log(errorResponse);
      }
    )
}
}
