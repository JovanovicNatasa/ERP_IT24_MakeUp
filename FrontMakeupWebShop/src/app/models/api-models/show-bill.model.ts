import { ShowShoppingCart } from "./show-shopping-basket.model";

export interface ShowBill{
  racunId: number;
	datumKupovine: string;
	vremeKupovine: string;
	iznosPost: string;
	iznosSaPost: number;
	iznosPopusta: number;
	iznosSaPopustom: number;
  status:string;
	korpaId: number;
  korpa:ShowShoppingCart;
}
