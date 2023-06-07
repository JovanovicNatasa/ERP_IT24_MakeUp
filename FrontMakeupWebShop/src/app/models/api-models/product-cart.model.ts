import { ShoppingCart } from './shopping-basket.model';
import { Product } from './product.model';

export interface ProductInCart {
  proizUkorpiId:number,
  brojKomada: number,
  proizvodId: number,
  proizvod: Product, // Add the product property of type Product
  korpaId: number,
  korpa: ShoppingCart
}
