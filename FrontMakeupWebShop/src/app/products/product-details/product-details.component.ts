import { Component, OnInit } from '@angular/core';
import { ProductService } from '../product.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {
  product: any;
  activeTab: string = 'sastav';
  quantity: number = 1;

  constructor(
    private readonly productService: ProductService,
    private readonly route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const proizvodId = params.get('proizvodId');
      if (proizvodId) {
        const idProizvoda = parseInt(proizvodId, 10);
        this.productService.getSingleProduct(idProizvoda)
          .subscribe(
            (successResponse) => {
              this.product = successResponse;
            }
          );
      }
    });
  }

  setActiveTab(tab: string): void {
    this.activeTab = tab;
  }

  incrementQuantity(): void {
    if (this.quantity < this.product.kolicinaNaStanju) {
      this.quantity++;
    }
  }

  decrementQuantity(): void {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }
}
