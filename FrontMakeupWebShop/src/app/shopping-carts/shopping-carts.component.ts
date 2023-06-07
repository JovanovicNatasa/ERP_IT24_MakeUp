import { Component, OnInit } from '@angular/core';
import { ShoppingCartsService } from './shopping-carts.service';
import { ProductInCart } from '../models/api-models/product-cart.model';
import { LoginService } from '../users/login/login.service';
import { ShoppingCart } from '../models/api-models/shopping-basket.model';
import { ProductDetailsService } from '../products/product-details/product-details.service';
import { switchMap } from 'rxjs';

@Component({
  selector: 'app-shopping-carts',
  templateUrl: './shopping-carts.component.html',
  styleUrls: ['./shopping-carts.component.css']
})
export class ShoppingCartsComponent implements OnInit {

  iznos:number|undefined;
  productsInCart: ProductInCart[] = []; // Array to store products in the cart
  korpa: ShoppingCart | undefined;
  korpaId = this.loginService.getKorpaId();




  constructor(
    private shoppingCartsService: ShoppingCartsService,
    private loginService: LoginService,
    private productDetailsService: ProductDetailsService
  ) {}

  ngOnInit(): void {
    this.productDetailsService.getProductInShoppingCartByKorpaId(this.korpaId).subscribe(
      (successResponse) => {
        this.productsInCart = successResponse;

      },
      (errorResponse) => {
        console.log(errorResponse);
      }
    );
    this.getShoppingCart();
  }

  getShoppingCart(): void {
    const korpaId = this.loginService.getKorpaId();

    this.shoppingCartsService.getShoppingCartById(korpaId)
      .subscribe(
        (cartResponse) => {
          this.korpa = cartResponse;
          this.iznos=cartResponse.ukupanIznos;
        },
        (errorResponse) => {
          console.error('Error retrieving shopping cart:', errorResponse);
        }
      );
  }


  setIznos(): number {
    return this.iznos || 0; //  default value in case `iznos` is undefined
  }

  shipping(iznos:number):number{
    if (iznos<=3000){
      return 500;
    }
    else return 0;
  }

  sum(osnovica:number,dostava:number){
    return osnovica+dostava;
  }

  calculateSumPerProduct(product:ProductInCart):number{
    const brojKomada = product.brojKomada;
    const cena= product.proizvod.cenaPoKom;
    return brojKomada*cena;

  }
  // shopping-carts.component.ts
  incrementQuantity(product: ProductInCart): void {
    if (product.brojKomada < product.proizvod.kolicinaNaStanju) {
      product.brojKomada++;
      this.updateQuantityInDatabase(product);
    }
  }

  decrementQuantity(product: ProductInCart): void {
    if (product.brojKomada > 1) {
      product.brojKomada--;
      this.updateQuantityInDatabase(product);
    }
  }

  updateQuantityInDatabase(product: ProductInCart): void {
    const proizvodUKId = product.proizUkorpiId;
    const updateProductRequest = {
      brojKomada: product.brojKomada,
      proizvodId: product.proizvod.proizvodId,
      korpaId: this.korpaId
    };

    this.productDetailsService.updateProductQuantity(updateProductRequest, proizvodUKId)
      .pipe(
        switchMap(() => this.shoppingCartsService.getShoppingCartById(this.korpaId))
      )
      .subscribe(
        (cartResponse) => {
          this.korpa = cartResponse;
          this.iznos = cartResponse.ukupanIznos;
        },
        (errorResponse) => {
          console.error('Error updating product quantity:', errorResponse);
        }
      );
  }

  removeProduct(product: ProductInCart): void {
    this.shoppingCartsService.removeProductFromCart(product.proizUkorpiId).subscribe(
      () => {
        // Remove the product from the array or update the product list.
        const index = this.productsInCart.indexOf(product);
        if (index > -1) {
          this.productsInCart.splice(index, 1);
        }
        // Update the cart information after removing the product.
        this.getShoppingCart();
      },
      (errorResponse) => {
        console.error('Error removing product:', errorResponse);
      }
    );
  }



}
