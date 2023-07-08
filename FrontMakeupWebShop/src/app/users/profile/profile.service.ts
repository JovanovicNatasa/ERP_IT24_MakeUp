import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/api-models/user.model'; // Import the Korisnik model
import { UpdateUser } from 'src/app/models/api-models/updateUser.model';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  private baseUri = 'https://localhost:44307';

  constructor(private httpClient: HttpClient) { }

  // Implement the getKorisnikByUsername method here
  getKorisnikByUsername(username: string): Observable<User> {
    return this.httpClient.get<User>(this.baseUri + '/Korisnik/username/' + username);
  }

  updateUser(userId: number, userInfo: UpdateUser): Observable<UpdateUser> {
    return this.httpClient.put<UpdateUser>(this.baseUri + '/Korisnik/' + userId + '/korisnik', userInfo);
  }





}
