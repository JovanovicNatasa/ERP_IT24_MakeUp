import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Type } from 'src/app/models/ui-models/type.model';
import { AdminService } from '../../admin.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-tip',
  templateUrl: './tip.component.html',
  styleUrls: ['./tip.component.css']
})
export class TipComponent implements OnInit{
  tip:Type[]=[];

  displayedColumns: string[] = ['nazivTipa'];
  dataSource: MatTableDataSource<Type>= new MatTableDataSource<Type>();

  constructor(private adminService: AdminService){}

@ViewChild(MatPaginator) matPaginator!:MatPaginator;
@ViewChild(MatSort) matSort!:MatSort;

  ngOnInit(): void {

    this.adminService.getaTip()
    .subscribe(
      (successResponse)=>{
        this.tip=successResponse;
        this.dataSource=new MatTableDataSource<Type>(this.tip);

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
