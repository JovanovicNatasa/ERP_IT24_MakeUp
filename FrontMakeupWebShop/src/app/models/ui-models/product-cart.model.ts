import { ShoppingCart } from '../api-models/shopping-basket.model';
import { Product } from '../api-models/product.model';

export interface ProductInCart {
  brojKomada: number,
  proizvodId: number,
  proizvod: Product,// Add the product property of type Product
  korpaId: number,
  korpa: ShoppingCart
}
