import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../admin.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Collection } from 'src/app/models/ui-models/collection.model';
import { Brand } from 'src/app/models/ui-models/brand.model';
import { Type } from 'src/app/models/ui-models/type.model';
import { Purpose } from 'src/app/models/ui-models/purpose.model';
import { Product } from 'src/app/models/ui-models/product.model';
import { ProductService } from 'src/app/products/product.service';
@Component({
  selector: 'app-update-proizvod',
  templateUrl: './update-proizvod.component.html',
  styleUrls: ['./update-proizvod.component.css']
})
export class UpdateProizvodComponent implements OnInit {
  proizvodId: number | undefined;
  product:Product={
    proizvodId:0,
    sastav :'',
    nacinUpotrebe:'',
    model:'',
	cenaPoKom:0,
    kolekcijaId:0,
    kolekcija:{

      kolekcijaId:0,
      nazivKolekcije:''

    },
    brendId:0,
    brend:{

      brendId: 0 ,
	    nazivBrenda: ''

    },
    namenaId:0,
    namena: {

      namenaId:0,
	    nazivNamene:''

    },
	tipId:0,
	tip:{
		tipId:0,
		nazivTipa:'',
	},
    kolicinaNaStanju:0
  }

  kolekcijaList:Collection[]=[];
  brendList:Brand[]=[];
  tipList:Type[]=[];
  namenaList:Purpose[]=[]

  constructor(private readonly productService: ProductService,private readonly adminService: AdminService, private readonly route: ActivatedRoute, private readonly snackBar:MatSnackBar, private router:Router) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const idParam = params.get('id');
      this.proizvodId = idParam ? Number(idParam) : undefined;

      if (this.proizvodId) {
        this.productService.getSingleProduct(this.proizvodId)
        .subscribe(
          (successResponse)=>{
            this.product=successResponse;
          }
        );
        this.adminService.getNamena()
        .subscribe(
          (successResponse)=>{
            this.namenaList=successResponse;
          }
        );
        this.adminService.getTip()
        .subscribe(
          (successResponse)=>{
            this.tipList=successResponse;
          }
        );
        this.adminService.getKolekcija()
        .subscribe(
          (successResponse)=>{
            this.kolekcijaList=successResponse;
          }
        );
		this.adminService.getBrends()
        .subscribe(
          (successResponse)=>{
            this.brendList=successResponse;
          }
        );

      }
    });
  }

  onUpdate():void{


    this.adminService.updateProizvod(this.product,this.product.proizvodId,)
    .subscribe(
      (successResponse)=>{
        this.snackBar.open('Proizvod updated successfully!', undefined, {
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
    this.adminService.deleteProizvod(this.product.proizvodId)
    .subscribe(
      (successResponse)=>{
        this.snackBar.open('Proizvod deleted successfully!', undefined, {
          duration:2000
        });
        setTimeout(()=>{

          this.router.navigateByUrl('Pregled-proizvoda');

        },2000)

      },
      (errorResponse)=>{

        this.snackBar.open('Delete failed', undefined, {
          duration:2000
        });
      }
    )
  }
}
