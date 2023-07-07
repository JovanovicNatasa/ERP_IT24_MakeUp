import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../models/api-models/product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private baseUri = 'https://localhost:44307';
  constructor(private httpClient:HttpClient) {}

  getProducts(): Observable<Product[]> {
    return this.httpClient.get<Product[]>(this.baseUri+'/Proizvod');
  }

  getSingleProduct(proizvodId: number): Observable<Product>{
    return this.httpClient.get<Product>(this.baseUri+'/Proizvod/'+proizvodId)
  }

}
