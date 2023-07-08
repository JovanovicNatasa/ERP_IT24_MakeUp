import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { User } from 'src/app/models/ui-models/user.model';
import { AdminService } from '../../admin.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-korisnik',
  templateUrl: './korisnik.component.html',
  styleUrls: ['./korisnik.component.css']
})
export class KorisnikComponent implements OnInit{
  korisnik:User[]=[];

  displayedColumns: string[] = ['ime', 'prezime', 'email', 'brojKupovina', 'grad', 'nazivUloge','edit'];
  dataSource: MatTableDataSource<User>= new MatTableDataSource<User>();

  constructor(private adminService: AdminService){}

@ViewChild(MatPaginator) matPaginator!:MatPaginator;
@ViewChild(MatSort) matSort!:MatSort;

  ngOnInit(): void {

    this.adminService.getKorisnik()
    .subscribe(
      (successResponse)=>{
        this.korisnik=successResponse;
        this.dataSource=new MatTableDataSource<User>(this.korisnik);

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
