export class ShoppingCart {
  korpaId: number;
  korisnikId: number;

  constructor(korpaId?: number, korisnikId?: number) {
    this.korpaId = korpaId !== undefined ? korpaId : 0;
    this.korisnikId = korisnikId !== undefined ? korisnikId : 0;
  }
}
