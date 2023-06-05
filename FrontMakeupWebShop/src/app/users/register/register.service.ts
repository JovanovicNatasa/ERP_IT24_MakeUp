import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/ui-models/user.model';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  private baseUri = 'https://localhost:44307';

  constructor(private httpClient: HttpClient) { }

  registerUser(registerData: User): Observable<any> {
    return this.httpClient.post<any>(this.baseUri + '/Korisnik/register', registerData);
  }
}
