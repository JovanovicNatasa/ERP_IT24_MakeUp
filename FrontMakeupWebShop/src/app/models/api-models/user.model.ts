export interface User {
  korisnikId:number;
  ime: string;
  prezime: string;
  jmbg: string;
  email: string;
  kontakt: string;
  username: string;
  lozinka: string;
  adresa: {
    adresaId: number;
    grad: string;
    ulica: string;
    broj: string;
    postanskiBroj: string;
  };
}
