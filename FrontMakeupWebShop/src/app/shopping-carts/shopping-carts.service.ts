import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ShoppingCart } from '../models/api-models/shopping-basket.model';
import { AddBill } from '../models/api-models/postBill.model';
import { AddedBill } from '../models/api-models/postBill.model';

@Injectable({
  providedIn: 'root'
})
export class ShoppingCartsService {
  private baseUri = 'https://localhost:44307';
  private racunId=0;

  constructor(private httpClient: HttpClient) { }


  getRacunId():number{
    return this.racunId;
  }

  setRacunId(racunId: number): void {
    this.racunId = racunId;
  }

  getShoppingCartById(korpaId: number): Observable<ShoppingCart> {
    return this.httpClient.get<ShoppingCart>(this.baseUri+'/Korpa/'+korpaId);
  }

  removeProductFromCart(proizvodUKId: number): Observable<any> {
    return this.httpClient.delete(this.baseUri+'/ProizvodUKorpi/'+proizvodUKId);
  }

  postBill(addRacunRequest: AddBill): Observable<AddBill> {
    this.setRacunId(addRacunRequest.racunId);
    return this.httpClient.post<AddBill>(this.baseUri + '/Racun', addRacunRequest);
  }

  getBillById(billId:number):Observable<AddedBill>{
    return this.httpClient.get<AddedBill>(this.baseUri + '/Racun/'+billId);
  }

  createPaymentIntent(racunId:number): Observable<AddedBill>{
    return this.httpClient.post<AddedBill>(this.baseUri +'/'+ racunId,racunId);
  }

}
