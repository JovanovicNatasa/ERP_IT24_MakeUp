import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../models/api-models/product.model';
import { Brand } from '../models/api-models/brand.model';
import { KolekcijaComponent } from './tables/kolekcija/kolekcija.component';
import { Collection } from '../models/api-models/collection.model';
import { Purpose } from '../models/api-models/purpose.model';
import { Type } from '../models/api-models/type.model';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  private baseUri = 'https://localhost:44307';
  constructor(private httpClient: HttpClient) { }

  getBrends(): Observable<Brand[]>{
    return this.httpClient.get<Brand[]>(this.baseUri+'/Brend')
  }
  getSingleBrend(idmodela:string): Observable<Brand> {
    return this.httpClient.get<Brand>(this.baseUri+'/Pregled-brenda/'+ idmodela)
  }
  addBrend(addModelRequest: Brand): Observable<any> {
    return this.httpClient.post(this.baseUri + '/Brend', addModelRequest);
  }


  getKolekcija(): Observable<Collection[]>{
    return this.httpClient.get<Collection[]>(this.baseUri+'/Kolekcija')
  }


  getNamena(): Observable<Purpose[]>{
    return this.httpClient.get<Purpose[]>(this.baseUri+'/api/Namena')
  }

  getaTip(): Observable<Type[]>{
    return this.httpClient.get<Type[]>(this.baseUri+'/Tip')
  }


}
