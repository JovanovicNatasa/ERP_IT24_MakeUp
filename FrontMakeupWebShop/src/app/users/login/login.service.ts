import { Injectable } from '@angular/core';
import { Observable, catchError, map, of, switchMap, tap, throwError } from 'rxjs';
import { Login } from 'src/app/models/api-models/login.model';
import { User } from 'src/app/models/api-models/user.model';
import { HttpClient, HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { ShoppingCart } from 'src/app/models/ui-models/shopping-basket.model';
import { CookieService } from 'ngx-cookie-service';
import { ShowShoppingCart } from 'src/app/models/api-models/show-shopping-basket.model';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private baseUri = 'https://localhost:44307';
  private loggedInUsername: string = '';
  private loggedInPassword: string = '';
  private isLoggedInBool = false;
  private korpaId: number = Number(this.cookieService.get('korpaId')) || 0;
  private korisnikId: number = 0;
  private uloga: number = 0;
  private shoppingCartSet = false;
private shoppingCart: ShowShoppingCart | null = null;

  constructor(private httpClient: HttpClient, private cookieService: CookieService) {
    this.checkLoginStatus(); // Check login status on initialization
    this.checkKorpaId(); // Check korpaId on initialization
  }

  setToken(token: string): void {
    const d = new Date();
    d.setTime(d.getTime() + (1 * 60 * 60 * 1000));
    this.cookieService.set('token', token, { expires: d });
  }

  getToken(): string | null {
    return this.cookieService.get('token');
  }


  setKorpaId(korpaId: number): void {
    this.korpaId = korpaId;
    const d = new Date();
    d.setTime(d.getTime() + (1 * 60 * 60 * 1000));
    this.cookieService.set('korpaId', korpaId.toString(), { expires: d });
  }

  getKorpaId(): number {
    return Number(this.cookieService.get('korpaId'));
  }

  loginUser(loginRequest: Login): Observable<string> {
    return this.httpClient.post(this.baseUri + '/Korisnik/login', loginRequest, { responseType: 'text' }).pipe(
      switchMap((response: string) => {
        this.setLoggedIn(true);
        this.korpaId = this.generateRandomKorpaId();
        this.setKorpaId(this.korpaId);
        this.setUsername(loginRequest.username);
        this.setPassword(loginRequest.lozinka);

        return this.fetchUlogaId().pipe(
          map((ulogaId: number) => {
            this.uloga = ulogaId;
            return response;
          })
        );
      })
    );
  }

  getUloga(): number {
    return this.uloga;
  }

  getUsername(): string {
    return this.loggedInUsername;
  }

  getPassword(): string {
    return this.loggedInPassword;
  }

  setUsername(username: string): void {
    this.loggedInUsername = username;
  }

  setPassword(password: string): void {
    this.loggedInPassword = password;
  }

  getLoggedIn(): boolean {
    return this.isLoggedInBool;
  }

  setLoggedIn(value: boolean): void {
    this.isLoggedInBool = value;
    this.cookieService.set('isLoggedIn', value.toString());
  }


  public generateRandomKorpaId(): number {
    return Math.floor(Math.random() * 1000);
  }

  private checkLoginStatus(): void {
    const isLoggedIn = this.cookieService.get('isLoggedIn');
    this.isLoggedInBool = isLoggedIn === 'true';
  }

  private checkKorpaId(): void {
    const korpaId = this.cookieService.get('korpaId');
    if (korpaId) {
      this.korpaId = Number(korpaId);
    }
  }


  private fetchKorisnikId(): Observable<number> {
    const username = this.loggedInUsername;

    return this.httpClient.get<User>(this.baseUri + '/Korisnik/username/' + username).pipe(
      map((response: User) => response.korisnikId),
      catchError((error: any) => {
        console.error('Error fetching korisnikId:', error);
        return of(0); // Return zero if an error occurs
      })
    );
  }

  private fetchUlogaId(): Observable<number> {
    const username = this.loggedInUsername;

    return this.httpClient.get<User>(this.baseUri + '/Korisnik/username/' + username).pipe(
      map((response: User) => response.ulogaId),
      catchError((error: any) => {
        console.error('Error fetching ulogaId:', error);
        return of(0); // Return zero if an error occurs
      })
    );
  }

  createKorpa(): Observable<any> {
    return this.fetchKorisnikId().pipe(
      switchMap((korisnikId: number) => {
        this.korisnikId = korisnikId;

        const korpaRequest: ShoppingCart = new ShoppingCart(this.korpaId, this.korisnikId);
        return this.httpClient.post(this.baseUri + '/Korpa', korpaRequest);
      }),
      catchError((error: any) => {
        console.error("Error creating korpa:", error);
        return throwError(error);
      })
    );
  }

  getKorisnikId(): number {
    return this.korisnikId;
  }

  logout(): void {
    console.log('Logout method called');
    this.cookieService.delete('token');
    this.cookieService.delete('korpaId');
    this.clearLocalStorage();
    this.clearKorisnikId();
    this.setLoggedIn(false); // Set isLoggedInBool to false
  }

  private clearLocalStorage() {
    localStorage.removeItem('id_token');
    localStorage.removeItem('expires_at');
    localStorage.removeItem('korisnik_id');
    localStorage.removeItem('korpaId')
  }

  private clearKorisnikId() {
    this.korisnikId = 0;
    this.korpaId=0;
  }
}
