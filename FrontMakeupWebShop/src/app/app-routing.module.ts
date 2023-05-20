import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductsComponent } from './products/products.component';
import { ProductDetailsComponent } from './products/product-details/product-details.component';
import { LoginComponent } from './users/login/login.component';
import { ShoppingCartsComponent } from './shopping-carts/shopping-carts.component';

const routes: Routes = [
  {
    path:'',
    component: ProductsComponent
  },
  {
    path:'Proizvod',
    component: ProductsComponent
  },
  {
    path:'Proizvod/:proizvodId',
    component:ProductDetailsComponent
  },
  {
    path:'Korisnik/login',
    component:LoginComponent
  },
  {
    path:'Korpa/:korpaId',
    component:ShoppingCartsComponent
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
