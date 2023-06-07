import { Component, OnInit, ViewChild} from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Collection } from 'src/app/models/ui-models/collection.model';
import { AdminService } from '../../admin.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';


@Component({
  selector: 'app-kolekcija',
  templateUrl: './kolekcija.component.html',
  styleUrls: ['./kolekcija.component.css']
})
export class KolekcijaComponent implements OnInit{
  kolekcija:Collection[]=[];

  displayedColumns: string[] = ['nazivKolekcije'];
  dataSource: MatTableDataSource<Collection>= new MatTableDataSource<Collection>();

  constructor(private adminService: AdminService){}

@ViewChild(MatPaginator) matPaginator!:MatPaginator;
@ViewChild(MatSort) matSort!:MatSort;

  ngOnInit(): void {

    this.adminService.getKolekcija()
    .subscribe(
      (successResponse)=>{
        this.kolekcija=successResponse;
        this.dataSource=new MatTableDataSource<Collection>(this.kolekcija);

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
