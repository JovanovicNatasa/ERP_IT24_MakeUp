import { Injectable } from '@angular/core';
import { Observable, catchError, map, of, switchMap, tap, throwError } from 'rxjs';
import { Login } from 'src/app/models/api-models/login.model';
import { User } from 'src/app/models/api-models/user.model';
import { HttpClient, HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { ShoppingCart } from 'src/app/models/ui-models/shopping-basket.model';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private baseUri = 'https://localhost:44307';
  private loggedInUsername: string = '';
  private isLoggedInBool = false;
  private korpaId: number = 0;
  private korisnikId: number = 0;

  constructor(private httpClient: HttpClient, private cookieService: CookieService) {
    this.checkLoginStatus(); // Check login status on initialization
  }

  setToken(token: string): void {
    const d = new Date();
  d.setTime(d.getTime() + (1 * 60 * 60 * 1000));
    this.cookieService.set('token', token, { expires: d });
  }

  getToken(): string | null {
    return this.cookieService.get('token');
  }

  loginUser(loginRequest: Login): Observable<string> {
    return this.httpClient.post(this.baseUri + '/Korisnik/login', loginRequest, { responseType: 'text' })
      .pipe(
        tap((response: string) => {
          this.setLoggedIn(true);
          this.korpaId = this.generateRandomKorpaId();
          this.setUsername(loginRequest.username);
        })
      );
  }

  getUsername(): string {
    return this.loggedInUsername;
  }

  setUsername(username: string): void {
    this.loggedInUsername = username;
  }

  getLoggedIn(): boolean {
    return this.isLoggedInBool;
  }

  setLoggedIn(value: boolean): void {
    this.isLoggedInBool = value;
    this.cookieService.set('isLoggedIn', value.toString());
  }

  getKorpaId(): number {
    return this.korpaId;
  }

  setKorpaId(korpaId: number): void {
    this.korpaId = korpaId;
  }

  private generateRandomKorpaId(): number {
    return Math.floor(Math.random() * 1000);
  }

  private checkLoginStatus(): void {
    const isLoggedIn = this.cookieService.get('isLoggedIn');
    this.isLoggedInBool = isLoggedIn === 'true';
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

  logout() {
    this.cookieService.delete('token');
    this.clearLocalStorage();
    this.clearKorisnikId();
    }

  private clearLocalStorage() {
    localStorage.removeItem('id_token');
    localStorage.removeItem('expires_at');
    }

  private clearKorisnikId() {
    this.korisnikId = 0;
    }
}

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private cookieService: CookieService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // Get the token from the cookie
    const token = this.cookieService.get('token');
    if (token) {
      const headers = request.headers.set('Authorization', `Bearer ${token}`);
      request = request.clone({ headers });
    }
    return next.handle(request);
  }
}

