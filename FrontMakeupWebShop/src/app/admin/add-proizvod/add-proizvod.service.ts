import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from 'src/app/models/api-models/product.model';

@Injectable({
  providedIn: 'root'
})
export class AddProizvodService
  {
    private apiUrl = 'https://localhost:44307/Proizvod';

  constructor(private http: HttpClient) {}

  addParfem(addParfemRequest: Product): Observable<any> {
    return this.http.post(this.apiUrl, addParfemRequest);
  }
  }

