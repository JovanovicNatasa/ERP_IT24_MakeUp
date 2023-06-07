import { Component, OnInit } from '@angular/core';
import { ProductService } from '../product.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductDetailsService } from './product-details.service';
import { LoginService } from 'src/app/users/login/login.service';
import { Product } from 'src/app/models/api-models/product.model';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {

  product!: Product;
  activeTab: string = 'sastav';
  quantity: number = 1;

  constructor(
    private readonly productService: ProductService,
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly productDetailsService:ProductDetailsService,
    private readonly loginService: LoginService // Inject the LoginService
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const proizvodId = params.get('proizvodId');
      if (proizvodId) {
        const idProizvoda = parseInt(proizvodId, 10);
        this.productService.getSingleProduct(idProizvoda)
          .subscribe(
            (successResponse) => {
              this.product = successResponse;
            },(errorResponse)=>{
              console.log(errorResponse);
            }
          );
      }
    });
  }

  setActiveTab(tab: string): void {
    this.activeTab = tab;
  }

 /* incrementQuantity(): void {
    if (this.quantity < this.product.kolicinaNaStanju) {
      this.quantity++;
    }
  }

  decrementQuantity(): void {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }*/
  incrementQuantity(): void {
    this.quantity = this.productDetailsService.incrementQuantity(this.quantity);
  }

  decrementQuantity(): void {
    this.quantity = this.productDetailsService.decrementQuantity(this.quantity);
  }

  addToCart(): void {
    const addProizvodUKorpiRequest = {
      brojKomada: this.quantity,
      proizvodId: this.product.proizvodId,
      korpaId: this.loginService.getKorpaId()
    };

    this.productDetailsService.addProductInShoppingCart(addProizvodUKorpiRequest)
      .subscribe(
        (successResponse) => {
          // Handle success response if needed
          console.log("Product added to shopping cart:", successResponse);
        },
        (errorResponse) => {
          // Handle error response if needed
          console.error("Error adding product to shopping cart:", errorResponse);
        }
      );
  }
}
