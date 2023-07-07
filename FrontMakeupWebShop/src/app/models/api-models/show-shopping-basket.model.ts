import { User } from "./user.model";

export interface ShowShoppingCart {
  korpaId: number;
  ukupanIznos?: number;
  brProizvoda?: number;
  popust?: boolean;
  procenatPop?: number;
  korisnikId: number;
  korisnik:User;
}
