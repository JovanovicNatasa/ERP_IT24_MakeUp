import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Purpose } from 'src/app/models/ui-models/purpose.model';
import { AdminService } from '../../admin.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-namena',
  templateUrl: './namena.component.html',
  styleUrls: ['./namena.component.css']
})
export class NamenaComponent implements OnInit{
  namena:Purpose[]=[];

  displayedColumns: string[] = ['nazivNamene','edit'];
  dataSource: MatTableDataSource<Purpose>= new MatTableDataSource<Purpose>();

  constructor(private adminService: AdminService){}

@ViewChild(MatPaginator) matPaginator!:MatPaginator;
@ViewChild(MatSort) matSort!:MatSort;

  ngOnInit(): void {

    this.adminService.getNamena()
    .subscribe(
      (successResponse)=>{
        this.namena=successResponse;
        this.dataSource=new MatTableDataSource<Purpose>(this.namena);

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
