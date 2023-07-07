
import { ShowShoppingCart } from "./show-shopping-basket.model";

export interface AddBill {
  racunId: number,
  korpaId: number,
}

export interface AddedBill{
  racunId: number,
	datumKupovine: string,
	vremeKupovine: string,
	iznosPost: string,
	iznosSaPost: number,
	iznosPopusta: number,
	iznosSaPopustom: number,
  status:string,
  clientSecret: string,
  paymentIntentId: string,
	korpaId: number,
  korpa:ShowShoppingCart,}
