import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ShoppingCart } from '../models/api-models/shopping-basket.model';

@Injectable({
  providedIn: 'root'
})
export class ShoppingCartsService {
  private baseUri = 'https://localhost:44307';

  constructor(private httpClient: HttpClient) { }


  getShoppingCartById(korpaId: number): Observable<ShoppingCart> {
    return this.httpClient.get<ShoppingCart>(this.baseUri+'/Korpa/'+korpaId);
  }

  removeProductFromCart(proizvodUKId: number): Observable<any> {
    return this.httpClient.delete(this.baseUri+'/ProizvodUKorpi/'+proizvodUKId);
  }

}
