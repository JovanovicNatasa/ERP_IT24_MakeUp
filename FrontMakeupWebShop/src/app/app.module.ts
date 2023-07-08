import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';


// Material Modules
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { MatSliderModule } from '@angular/material/slider';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatMenuModule } from '@angular/material/menu';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatListModule } from '@angular/material/list';
import { MatStepperModule } from '@angular/material/stepper';
import { MatTabsModule } from '@angular/material/tabs';
import { MatTreeModule } from '@angular/material/tree';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatBadgeModule } from '@angular/material/badge';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatRippleModule } from '@angular/material/core';
import { MatBottomSheetModule } from '@angular/material/bottom-sheet';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';

import { TopNavComponent } from './layout/top-nav/top-nav.component';
import { ProductsComponent } from './products/products.component';
import { SearchComponent } from './search/search.component';
import { ProductDetailsComponent } from './products/product-details/product-details.component';
import { ClickOutsideDirective } from './clickOutside.directive';
import { LoginComponent } from './users/login/login.component';
import { RegisterComponent } from './users/register/register.component';
import { ProfileComponent } from './users/profile/profile.component';
import { ShoppingCartsComponent } from './shopping-carts/shopping-carts.component';
import { LoginService } from './users/login/login.service';
import { JwtInterceptor } from './users/jwt.interceptor';
import { JWT_OPTIONS, JwtHelperService } from '@auth0/angular-jwt';
import { CookieService } from 'ngx-cookie-service';
import { AddBrendComponent } from './admin/add-brend/add-brend.component';
import { AddTipComponent } from './admin/add-tip/add-tip.component';
import { AddNamenaComponent } from './admin/add-namena/add-namena.component';
import { AddKolekcijaComponent } from './admin/add-kolekcija/add-kolekcija.component';
import { AddProizvodComponent } from './admin/add-proizvod/add-proizvod.component';
import { PanelComponent } from './admin/panel/panel.component';
import { BrendComponent } from './admin/tables/brend/brend.component';
import { KolekcijaComponent } from './admin/tables/kolekcija/kolekcija.component';
import { NamenaComponent } from './admin/tables/namena/namena.component';
import { TipComponent } from './admin/tables/tip/tip.component';
import { ProizvodComponent } from './admin/tables/proizvod/proizvod.component';
import { ShippingInformationComponent } from './shopping-carts/shipping-information/shipping-information.component';
import { PaymentComponent } from './shopping-carts/payment/payment.component';
import { SuccessDialogComponent } from './dialog/success-dialog/success-dialog.component';
import { ErrorDialogComponent } from './dialog/error-dialog/error-dialog.component';
import { NotifierComponent } from './dialog/notifier/notifier.component';
import { UpdateBrendComponent } from './admin/update/update-brend/update-brend.component';
import { UpdateKolekcijaComponent } from './admin/update/update-kolekcija/update-kolekcija.component';
import { UpdateNamenaComponent } from './admin/update/update-namena/update-namena.component';
import { UpdateProizvodComponent } from './admin/update/update-proizvod/update-proizvod.component';
import { UpdateTipComponent } from './admin/update/update-tip/update-tip.component';
import { KorisnikComponent } from './admin/tables/korisnik/korisnik.component';
import { RacunComponent } from './admin/tables/racun/racun.component';
import { UpdateUlogaKorisnikComponent } from './admin/update/update-uloga-korisnik/update-uloga-korisnik.component';







@NgModule({
  declarations: [
    AppComponent,
    TopNavComponent,
    ProductsComponent,
    SearchComponent,
    ProductDetailsComponent,
    ClickOutsideDirective,
    LoginComponent,
    RegisterComponent,
    ProfileComponent,
    ShoppingCartsComponent,
    AddBrendComponent,
    AddTipComponent,
    AddNamenaComponent,
    AddKolekcijaComponent,
    AddProizvodComponent,
    PanelComponent,
    BrendComponent,
    KolekcijaComponent,
    NamenaComponent,
    TipComponent,
    ProizvodComponent,
    ShippingInformationComponent,
    PaymentComponent,
    SuccessDialogComponent,
    ErrorDialogComponent,
    NotifierComponent,
    UpdateBrendComponent,
    UpdateKolekcijaComponent,
    UpdateNamenaComponent,
    UpdateProizvodComponent,
    UpdateTipComponent,
    KorisnikComponent,
    RacunComponent,
    UpdateUlogaKorisnikComponent
  ],
  entryComponents: [
    SuccessDialogComponent,
    ErrorDialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MatAutocompleteModule,
    MatCheckboxModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatFormFieldModule,
    MatInputModule,
    MatRadioModule,
    MatSelectModule,
    MatSliderModule,
    MatSlideToggleModule,
    MatMenuModule,
    MatSidenavModule,
    MatToolbarModule,
    MatCardModule,
    MatDividerModule,
    MatExpansionModule,
    MatGridListModule,
    MatListModule,
    MatStepperModule,
    MatTabsModule,
    MatTreeModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatBadgeModule,
    MatChipsModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatProgressBarModule,
    MatRippleModule,
    MatBottomSheetModule,
    MatDialogModule,
    MatSnackBarModule,
    MatTooltipModule,
    MatPaginatorModule,
    MatSortModule,
    MatTableModule,
    ToastrModule.forRoot()
  ],
  providers: [
    LoginService,
    JwtInterceptor,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: JWT_OPTIONS, useValue: JWT_OPTIONS },
    JwtHelperService,
    CookieService
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  bootstrap: [AppComponent]
})
export class AppModule {}
