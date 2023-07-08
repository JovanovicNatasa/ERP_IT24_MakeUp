import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Brand } from '../models/api-models/brand.model';
import { Collection } from '../models/api-models/collection.model';
import { Purpose } from '../models/api-models/purpose.model';
import { Type } from '../models/api-models/type.model';
import { AddBrendRequest } from '../models/api-models/brand.model';
import { AddKolekcijaRequest } from '../models/api-models/collection.model';
import { AddNamenaRequest } from '../models/api-models/purpose.model';
import { AddTipRequest } from '../models/api-models/type.model';
import { AddProizvodRequest } from '../models/api-models/product.model';
import { UpdateProizvodRequest } from '../models/api-models/product.model';
import { ShowBill } from '../models/api-models/show-bill.model';
import { Uloga, UpdateKorisnikUlogaRequest, User } from '../models/ui-models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  private baseUri = 'https://localhost:44307';
  constructor(private httpClient: HttpClient) { }

  getBrends(): Observable<Brand[]>{
    return this.httpClient.get<Brand[]>(this.baseUri+'/Brend')
  }
  getSingleBrend(brendId:number): Observable<Brand> {
    return this.httpClient.get<Brand>(this.baseUri+'/Brend/'+ brendId)
  }
  addBrend(addBrandRequest: AddBrendRequest): Observable<any> {
    return this.httpClient.post(this.baseUri + '/Brend', addBrandRequest);
  }
  updateBrend(addBrandRequest:AddBrendRequest, brendId:number):Observable<any>{
    return this.httpClient.put(this.baseUri +'/Brend/' + brendId, addBrandRequest)
  }
  deleteBrend(brendId:number):Observable<any>{
    return this.httpClient.delete(this.baseUri +'/Brend/' + brendId)
  }

  getKolekcija(): Observable<Collection[]>{
    return this.httpClient.get<Collection[]>(this.baseUri+'/Kolekcija')
  }
  getSingleKolekcija(kolekcijaId:number): Observable<Collection> {
    return this.httpClient.get<Collection>(this.baseUri+'/Kolekcija/'+ kolekcijaId)
  }
  addKolekcija(addKolekcijaRequest: AddKolekcijaRequest): Observable<any> {
    return this.httpClient.post(this.baseUri + '/Kolekcija', addKolekcijaRequest);
  }
  updateKolekcija(addKolekcijaRequest:AddKolekcijaRequest, kolekcijaId:number):Observable<any>{
    return this.httpClient.put(this.baseUri +'/Kolekcija/' + kolekcijaId, addKolekcijaRequest)
  }
  deleteKolekcija(kolekcijaId:number):Observable<any>{
    return this.httpClient.delete(this.baseUri +'/Kolekcija/' + kolekcijaId)
  }


  getNamena(): Observable<Purpose[]>{
    return this.httpClient.get<Purpose[]>(this.baseUri+'/api/Namena')
  }
  getSingleNamena(namenaId:number): Observable<Purpose> {
    return this.httpClient.get<Purpose>(this.baseUri+'/api/Namena/'+ namenaId)
  }
  addNamena(addNamenaRequest: AddNamenaRequest): Observable<any> {
    return this.httpClient.post(this.baseUri + '/api/Namena', addNamenaRequest);
  }
  updateNamena(addNamenaRequest:AddNamenaRequest, namenaId:number):Observable<any>{
    return this.httpClient.put(this.baseUri +'/api/Namena/' + namenaId, addNamenaRequest)
  }
  deleteNamena(namenaId:number):Observable<any>{
    return this.httpClient.delete(this.baseUri +'/api/Namena/' + namenaId)
  }

  getTip(): Observable<Type[]>{
    return this.httpClient.get<Type[]>(this.baseUri+'/Tip')
  }
  getSingleTip(tipId:number): Observable<Type> {
    return this.httpClient.get<Type>(this.baseUri+'/Tip/'+ tipId)
  }
  addTip(addTipRequest: AddTipRequest): Observable<any> {
    return this.httpClient.post(this.baseUri + '/Tip', addTipRequest);
  }
  updateTip(addTipRequest:AddTipRequest, tipId:number):Observable<any>{
    return this.httpClient.put(this.baseUri +'/Tip/' + tipId, addTipRequest)
  }
  deleteTip(tipId:number):Observable<any>{
    return this.httpClient.delete(this.baseUri +'/Tip/' + tipId)
  }


  addProizvod(addProizvodRequest: AddProizvodRequest): Observable<any> {
    return this.httpClient.post(this.baseUri + '/Proizvod', addProizvodRequest);
  }

  updateProizvod(updateProizvodRequest:UpdateProizvodRequest,proizvodId:number): Observable<any> {
    return this.httpClient.put(this.baseUri + '/Proizvod/' + proizvodId, updateProizvodRequest);
  }
  deleteProizvod(proizvodId:number):Observable<any>{
    return this.httpClient.delete(this.baseUri +'/Proizvod/' + proizvodId)
  }

  getRacun():Observable<ShowBill[]>{
    return this.httpClient.get<ShowBill[]>(this.baseUri+'/Racun')
  }

  getKorisnik():Observable<User[]>{
    return this.httpClient.get<User[]>(this.baseUri+'/Korisnik')
  }
  getSingleKorisnik(korisnikId:number): Observable<User> {
    return this.httpClient.get<User>(this.baseUri+'/Korisnik/'+ korisnikId)
  }

  updateUlogaKorisnik(korisnikId: number, request: UpdateKorisnikUlogaRequest): Observable<any> {
    return this.httpClient.put(this.baseUri + '/Korisnik/' + korisnikId + '/uloga', request);
  }

  deleteUser(korisnikId:number):Observable<any>{
    return this.httpClient.delete(this.baseUri +'/Korisnik/' + korisnikId)
  }

  getUloga():Observable<Uloga[]>{
    return this.httpClient.get<Uloga[]>(this.baseUri + '/api/Uloga')
  }
  /*uploadImage(file: FormData, proizvodId: number): Observable<any> {
    return this.httpClient.post<any>(`https://localhost:44307/Proizvod/upload-image/${proizvodId}`, file); // Promenite URL prema novoj putanji
  }*/
  
  
  
}
