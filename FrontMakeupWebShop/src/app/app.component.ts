import { Component, OnInit } from '@angular/core';
import { AccountService } from './users/account.service';
import { CookieService } from 'ngx-cookie-service';
import { LoginService } from './users/login/login.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  constructor(private accountService: AccountService, private cookieService: CookieService,private loginService:LoginService) {}

  ngOnInit(): void {
    this.initializeUser();
    this.initializeShoppingCart();
  }

  private initializeUser() {
    console.log('Initializing user...');
    const token = this.cookieService.get('token');
    if (token) {
      const decodedToken = this.accountService.getDecodedAccessToken(token);
      if (decodedToken) {
        const username = Object.values(decodedToken)[0] as string;
        this.accountService.loadCurrentKorisnik(token, username).subscribe((korisnik) => {
          this.loginService.setLoggedIn(true);
          const username=korisnik?.username || '';
          this.loginService.setUsername(username);
          console.log('Logged in user:', korisnik);
        });
      } else {
        // Invalid token, clear the cookie and set login status to false
        this.cookieService.delete('token');
        this.loginService.setLoggedIn(false);
      }
    } else {
      this.loginService.setLoggedIn(false);
    }
  }

  private initializeShoppingCart(){
    const korpaId = Number(this.cookieService.get('korpaId'));
    if(korpaId){
      this.accountService.loadCurrentKorpa(korpaId).subscribe((korpa)=>{
        const korpaId=korpa?.korpaId || 0;
        this.loginService.setKorpaId(korpaId);
      });
    }else{
      this.cookieService.delete('korpaId');
    }
  }


}
