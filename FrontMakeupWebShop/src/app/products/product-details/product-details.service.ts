import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ProductDetails } from 'src/app/models/api-models/product-details.model';
import { LoginService } from 'src/app/users/login/login.service';
import { ProductInCart } from 'src/app/models/api-models/product-cart.model';

@Injectable({
  providedIn: 'root'
})
export class ProductDetailsService {
  private baseUri = 'https://localhost:44307';

  constructor(
    private httpClient: HttpClient,
    private loginService: LoginService // Inject the LoginService
  ) {}
  addProductInShoppingCart(request: ProductDetails): Observable<ProductDetails> {
    return this.httpClient.post<any>(this.baseUri + '/ProizvodUKorpi', request);
  }


  getProductInShoppingCartByKorpaId(korpaId: number): Observable<ProductInCart[]> {
    return this.httpClient.get<ProductInCart[]>(this.baseUri + '/ProizvodUKorpi/korpa/' + korpaId);
  }

  // product-details.service.ts

updateProductQuantity(request: ProductDetails,proizvodUKId:number): Observable<any> {
  return this.httpClient.put<any>(this.baseUri + '/ProizvodUKorpi/'+proizvodUKId, request);
}



  incrementQuantity(quantity: number): number {
    return quantity + 1;
  }

  decrementQuantity(quantity: number): number {
    if (quantity > 1) {
      return quantity - 1;
    } else {
      return quantity;
    }
  }
}
