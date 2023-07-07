export class User {
  korisnikId:number;
  ime: string;
  prezime: string;
  email: string;
  kontakt: string;
  username: string;
  lozinka: string;
  adresa: Address;
  uloga:Uloga;

  constructor() {
    this.korisnikId=0;
    this.ime = '';
    this.prezime = '';
    this.email = '';
    this.kontakt = '';
    this.username = '';
    this.lozinka = '';
    this.adresa = new Address();
    this.uloga=new Uloga();
  }
}

export class Address {
  adresaId: number;
  grad: string;
  ulica: string;
  broj: string;
  postanskiBroj: number;

  constructor() {
    this.adresaId = 0;
    this.grad = '';
    this.ulica = '';
    this.broj = '';
    this.postanskiBroj = 0;
  }
}

  export class Uloga {
    ulogaId:number;
    nazivUloge:string;
    kratakOpis :string;
    sifra : string;

    constructor() {
      this.ulogaId=0;
      this.nazivUloge='';
      this.kratakOpis='';
      this.sifra ='';
    }
  }
