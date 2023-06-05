export class User {
  korisnikId:number;
  ime: string;
  prezime: string;
  email: string;
  kontakt: string;
  username: string;
  lozinka: string;
  adresa: Address;

  constructor() {
    this.korisnikId=0;
    this.ime = '';
    this.prezime = '';
    this.email = '';
    this.kontakt = '';
    this.username = '';
    this.lozinka = '';
    this.adresa = new Address();
  }
}

export class Address {
  adresaId: number;
  grad: string;
  ulica: string;
  broj: string;
  postanskiBroj: string;

  constructor() {
    this.adresaId = 0;
    this.grad = '';
    this.ulica = '';
    this.broj = '';
    this.postanskiBroj = '';
  }
}
