import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { Stripe, StripeCardCvcElement, StripeCardExpiryElement, StripeCardNumberElement, loadStripe } from '@stripe/stripe-js';
import { ShoppingCartsService } from '../shopping-carts.service';
import { SuccessDialogComponent } from 'src/app/dialog/success-dialog/success-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { ShoppingCart } from 'src/app/models/ui-models/shopping-basket.model';
import{ToastrService} from 'ngx-toastr'
import { LoginService } from 'src/app/users/login/login.service';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent implements OnInit {
  @Input() paymentForm!: FormGroup;
  @ViewChild('cardNumber') cardNumberElement?: ElementRef;
  @ViewChild('cardExpiry') cardExpiryElement?: ElementRef;
  @ViewChild('cardCvc') cardCvcElement?: ElementRef;

  submitted = false;
  stripe : Stripe | null = null;
  cardNumber?:StripeCardNumberElement;
  cardExpiry?:StripeCardExpiryElement;
  cardCvc?:StripeCardCvcElement;
  cardErrors:any;
  clientSecret:string='';
  billId:number=0;
  errorMes?:string='';
  korpaId:number=0;
  korpaRequest=new ShoppingCart();
  cardNumberComplete: boolean = false;
  cardExpiryComplete: boolean = false;
  cardCvcComplete: boolean = false;
  nameOnCardComplete:boolean=false;

  constructor(private formBuilder: FormBuilder, private shopingService:ShoppingCartsService,private dialog: MatDialog,private toastr : ToastrService, private loginService:LoginService) {
    this.paymentForm = this.formBuilder.group({
      nameOnCard: ['', Validators.required],
      cardNumber: ['', [Validators.required, Validators.pattern('[0-9]{16}')]],
      cardExpiry: ['', [Validators.required, Validators.pattern('^(0[1-9]|1[0-2])\/?([0-9]{4}|[0-9]{2})$')]],
      cardCvc: ['', [Validators.required, Validators.pattern('^[0-9]{3}$')]],
    });
  }


  ngOnInit(): void {
  loadStripe('pk_test_51NGK1cDXBd7seLTkinON0zHd3SRqmEWerId8Z9gRtVxfXemruLIT8zMfEcv7V1Sh8Qx33GdBxAh5Bh5K1cHWRd6Y00eQcX0NHq').then(stripe=>{
      this.stripe=stripe;
      const elements = stripe?.elements();
      if (elements){
        this.cardNumber=elements.create('cardNumber');
        this.cardNumber.mount(this.cardNumberElement?.nativeElement);
        this.cardNumber.on('change',event =>{
          this.cardNumberComplete=event.complete;
          if(event.error) this.cardErrors=event.error.message;
          else this.cardErrors=null;
        })

        this.cardExpiry=elements.create('cardExpiry');
        this.cardExpiry.mount(this.cardExpiryElement?.nativeElement);
        this.cardExpiry.on('change',event =>{
          this.cardExpiryComplete=event.complete;
          if(event.error) this.cardErrors=event.error.message;
          else this.cardErrors=null;
        })

        this.cardCvc=elements.create('cardCvc');
        this.cardCvc.mount(this.cardCvcElement?.nativeElement);
        this.cardCvc.on('change',event =>{
          this.cardCvcComplete=event.complete;
          if(event.error) this.cardErrors=event.error.message;
          else this.cardErrors=null;
        })
      }
    })
    this.billId=this.shopingService.getRacunId();
  }

  get formControls() {
    return this.paymentForm.controls;
  }

  get paymentFormComplete(){
    if(!this.paymentForm.get('nameOnCard')?.hasError('required')){
      this.nameOnCardComplete=true;
    }
    return this.nameOnCardComplete
    && this.cardNumberComplete
    && this.cardExpiryComplete
    && this.cardCvcComplete
  }


  openSuccessDialog() {
    const dialogRef = this.dialog.open(SuccessDialogComponent, {
      width: '250px',
      data: 'Zahvaljujemo se, Vaša porudžbina je uspešno primeljena' // Pass the message to the dialog component
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      // Perform any additional actions after the dialog is closed (if needed)
    });
  }





  onSubmit(): void {
    this.submitted = true;

    this.shopingService.getBillById(this.billId).subscribe(
      (bill) => {
        this.clientSecret = bill.clientSecret;

        this.stripe?.confirmCardPayment(this.clientSecret, {
          payment_method: {
            card: this.cardNumber!,
            billing_details: {
              name: this.paymentForm.get('nameOnCard')?.value
            }
          }
        }).then(result => {
          console.log(result);
          if (result.paymentIntent) {
            this.openSuccessDialog();
            this.korpaId = this.loginService.generateRandomKorpaId();
            this.loginService.setKorpaId(this.korpaId);
            this.createKorpa();
          } else {
            this.toastr.error(result.error.message);
          }
        })
      });
  }


  submitForm(): void {
    this.onSubmit();
  }

  createKorpa(): void {
    // Get the korisnikId from the LoginService
    const korisnikId = this.loginService.getKorisnikId();
    // Get the korpaId from the LoginService
    const korpaId = this.loginService.getKorpaId();
    this.korpaRequest.korisnikId = korisnikId;
    this.korpaRequest.korpaId = korpaId;

    console.log('User ID:', korisnikId);

    try {
      // Call the API to create the "korpa"
      this.loginService.createKorpa().subscribe(
        (response: any) => {
          // Log the response to the console
          console.log("Korpa created:", response);
          localStorage.setItem('korpaId', korpaId.toString());
        },
        (error: any) => {
          // Handle error while creating "korpa"
          console.error("Error creating korpa:", error);
        }
      );
    } catch (error) {
      // Handle error while creating "korpa"
      console.error("Error creating korpa:", error);
    }
  }


}
