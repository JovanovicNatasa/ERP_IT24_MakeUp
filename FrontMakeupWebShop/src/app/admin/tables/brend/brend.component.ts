import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Brand } from 'src/app/models/ui-models/brand.model';
import { AdminService } from '../../admin.service';

@Component({
  selector: 'app-brend',
  templateUrl: './brend.component.html',
  styleUrls: ['./brend.component.css']
})
export class BrendComponent implements OnInit{
  brend:Brand[]=[];

  displayedColumns: string[] = ['nazivBrenda'];
  dataSource: MatTableDataSource<Brand>= new MatTableDataSource<Brand>();

  constructor(private adminService: AdminService){}

@ViewChild(MatPaginator) matPaginator!:MatPaginator;
@ViewChild(MatSort) matSort!:MatSort;

  ngOnInit(): void {

    this.adminService.getBrends()
    .subscribe(
      (successResponse)=>{
        this.brend=successResponse;
        this.dataSource=new MatTableDataSource<Brand>(this.brend);

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
