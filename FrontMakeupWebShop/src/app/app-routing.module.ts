import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductsComponent } from './products/products.component';
import { ProductDetailsComponent } from './products/product-details/product-details.component';
import { LoginComponent } from './users/login/login.component';
import { ShoppingCartsComponent } from './shopping-carts/shopping-carts.component';
import { RegisterComponent } from './users/register/register.component';
import { AddBrendComponent } from './admin/add-brend/add-brend.component';
import { AddKolekcijaComponent } from './admin/add-kolekcija/add-kolekcija.component';
import { AddNamenaComponent } from './admin/add-namena/add-namena.component';
import { AddTipComponent } from './admin/add-tip/add-tip.component';
import { AddProizvodComponent } from './admin/add-proizvod/add-proizvod.component';

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
    path: 'Korisnik/register',
    component:RegisterComponent},
  {
     path: 'Korisnik/login',
     component: LoginComponent },
  {
    path:'Korpa/:korpaId',
    component:ShoppingCartsComponent
  },
  {
    path:'Brend',
    component:AddBrendComponent
  },
  {
    path:'Kolekcija',
    component:AddKolekcijaComponent
  },
  {
    path:'Namena',
    component:AddNamenaComponent
  },
  {
    path:'Tip',
    component:AddTipComponent
  },
  {
    path:'Proizvodadd',
    component:AddProizvodComponent
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
