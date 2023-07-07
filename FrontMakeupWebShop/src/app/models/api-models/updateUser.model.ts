export interface UpdateUser {
  ime: string;
  prezime: string;
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
