import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ProductDetails} from 'src/app/models/api-models/product-details.model';
import { LoginService } from 'src/app/users/login/login.service';

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
    const token = localStorage.getItem('token'); // Get the token from local storage

    // Create the headers object and include the authorization token
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    // Set the headers in the request options
    const options = { headers: headers };

    return this.httpClient.post<any>(this.baseUri + '/ProizvodUKorpi', request, options);
  }

}
