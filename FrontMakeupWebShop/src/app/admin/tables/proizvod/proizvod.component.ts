import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Brand } from 'src/app/models/ui-models/brand.model';
import { Collection } from 'src/app/models/ui-models/collection.model';
import { Product } from 'src/app/models/ui-models/product.model';
import { Purpose } from 'src/app/models/ui-models/purpose.model';
import { Type } from 'src/app/models/ui-models/type.model';
import { AdminService } from '../../admin.service';
import { ProductService } from 'src/app/products/product.service';

@Component({
  selector: 'app-proizvod',
  templateUrl: './proizvod.component.html',
  styleUrls: ['./proizvod.component.css']
})
export class ProizvodComponent implements OnInit{

  proizvod:Product[]=[];
  namena:Purpose[]=[];
  brend:Brand[]=[];
  tip: Type[]=[];
  kolekcija: Collection[]=[];
  displayedColumns: string[] = ['nazivNamene','nazivBrenda', 'nazivKolekcije','model',  'nazivTipa', 'cena','kolicinaNaStanju','edit'];
  dataSource: MatTableDataSource<Product>= new MatTableDataSource<Product>();

@ViewChild(MatPaginator) matPaginator!:MatPaginator;
@ViewChild(MatSort) matSort!:MatSort;



  constructor(private adminService: AdminService, private productService: ProductService){}

  ngOnInit(): void {

    this.productService.getProducts()
    .subscribe(
      (successResponse)=>{
        this.proizvod=successResponse;
        this.dataSource=new MatTableDataSource<Product>(this.proizvod);

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

    this.adminService.getBrends()
    .subscribe(
      (successResponse)=>{
        this.brend=successResponse
      },
      (errorResponse)=>{
        console.log(errorResponse);
      }
    )

    this.adminService.getKolekcija()
    .subscribe(
      (successResponse)=>{
        this.kolekcija=successResponse
      },
      (errorResponse)=>{
        console.log(errorResponse);
      }
    )
    this.adminService.getTip()
    .subscribe(
      (successResponse)=>{
        this.tip=successResponse
      },
      (errorResponse)=>{
        console.log(errorResponse);
      }
    )

    this.adminService.getNamena()
    .subscribe(
      (successResponse)=>{
        this.namena=successResponse
      },
      (errorResponse)=>{
        console.log(errorResponse);
      }
    )



  }

}
