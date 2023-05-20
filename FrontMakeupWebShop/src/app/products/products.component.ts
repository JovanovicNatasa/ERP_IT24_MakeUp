import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { ProductService } from './product.service';
import { Product } from '../models/ui-models/product.model';
import { Subscription, interval } from 'rxjs';

import { SearchService } from '../search/serach-s.service';
import { FilterService } from '../filter.service';
import { MatPaginator, PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit, OnDestroy {
  proizvod: Product[] = [];
  filteredProizvod: Product[] = [];
  searchText: string = '';
  noMatchingProductsMessage: string = '';
  images = ["/assets/banner-1.jpg","/assets/banner-2.jpg","/assets/banner-3.jpg","/assets/banner-4.jpg"];
  activeImageIndex = 0;
  autoScrollInterval = 2000;
  autoScrollSubscription: Subscription | undefined;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  sortDirection: string = '';
  filteredFlag=false;

  constructor(
    private productService: ProductService,
    private searchService: SearchService,
    private filterService: FilterService
  ) {}

  ngOnInit(): void {
    // Fetch products
    this.productService.getProducts().subscribe(
      (successResponse) => {
        this.proizvod = successResponse;
        this.filteredProizvod = this.proizvod; // Initialize with all products
      },
      (errorResponse) => {
        console.log(errorResponse);
      }
    );

    this.startAutoScroll();

    // Subscribe to search text changes
    this.searchService.searchText$.subscribe((searchText: string) => {
      this.searchText = searchText;
      this.filterProducts();
    });

     // Subscribe to filter changes
     this.filterService.brandFilter$.subscribe((brands: string[]) => {
      this.filterProductsByBrand(brands);
    });

    //
    this.filterService.typeFilter$.subscribe((types: string[]) => {
      this.filterProductsByType(types);
    });
  }

  ngOnDestroy(): void {
    this.stopAutoScroll();
  }

  filterProducts(): void {
    const searchTerm = this.searchText.toLowerCase().trim();

    this.filteredProizvod = this.proizvod.filter((product: Product) => {
      const brandName = product.brend ? product.brend.nazivBrenda.toLowerCase() : '';
      const kolekcijaName = product.kolekcija ? product.kolekcija.nazivKolekcije.toLowerCase() : '';
      const modelName = product.model.toLowerCase();
      const tipNaziv = product.tip ? product.tip.nazivTipa.toLowerCase() : '';

      return (
        brandName.includes(searchTerm) ||
        kolekcijaName.includes(searchTerm) ||
        modelName.includes(searchTerm) ||
        tipNaziv.includes(searchTerm)
      );
    });

    if (this.filteredProizvod.length === 0) {
      this.noMatchingProductsMessage = 'Trenutno na stanju nema proizvoda koje tražite :(';
    } else {
      this.noMatchingProductsMessage = '';
    }
  }

  filterProductsByBrand(brands: string[]): void {
    if (brands && brands.length > 0) {
      this.filteredProizvod = this.proizvod.filter(product =>
        brands.includes(product.brend?.nazivBrenda)
      );
    } else {
      this.filteredProizvod = this.proizvod;
    }

    if (this.filteredProizvod.length === 0) {
      this.noMatchingProductsMessage = 'Trenutno na stanju nema proizvoda koje tražite :(';
    } else {
      this.noMatchingProductsMessage = '';
    }
  }

  filterProductsByType(types: string[]): void {
    if (types && types.length > 0) {
      this.filteredProizvod = this.proizvod.filter(product =>
        types.includes(product.tip?.nazivTipa)
      );
    } else {
      this.filteredProizvod = this.proizvod;
    }

    if (this.filteredProizvod.length === 0) {
      this.noMatchingProductsMessage = 'Trenutno na stanju nema proizvoda koje tražite :(';
    } else {
      this.noMatchingProductsMessage = '';
    }
  }


  goToImage(index: number): void {
    this.activeImageIndex = index;
  }

  getCurrentImage(): string {
    return this.images[this.activeImageIndex];
  }

  nextImage(): void {
    this.activeImageIndex = (this.activeImageIndex + 1) % this.images.length;
  }

  prevImage(): void {
    this.activeImageIndex = (this.activeImageIndex - 1 + this.images.length) % this.images.length;
  }

  startAutoScroll(): void {
    this.stopAutoScroll(); // Stop any existing auto-scroll subscription

    this.autoScrollSubscription = interval(this.autoScrollInterval).subscribe(() => {
      this.nextImage();
    });
  }

  stopAutoScroll(): void {
    if (this.autoScrollSubscription) {
      this.autoScrollSubscription.unsubscribe();
    }
  }

  ngAfterViewInit(): void {
    this.paginator.page.subscribe((event: PageEvent) => {
      this.updatePagination();
    });
  }

  updatePagination(): void {
    const startIndex = this.paginator.pageIndex * this.paginator.pageSize;
    const endIndex = startIndex + this.paginator.pageSize;
    this.filteredProizvod = this.proizvod.slice(startIndex, endIndex);
  }

  onPageChange(event: PageEvent): void {
    this.paginator.pageSize = event.pageSize;
    this.paginator.pageIndex = event.pageIndex;
    this.updatePagination();
  }

  sortProductsPrice(sortDirection: string): void {
    this.sortDirection = sortDirection;

    if (sortDirection === 'descending') {
      this.filteredProizvod.sort((a, b) => a.cenaPoKom - b.cenaPoKom); // Sort by lowest cena
    } else if (sortDirection === 'ascending') {
      this.filteredProizvod.sort((a, b) => b.cenaPoKom - a.cenaPoKom); // Sort by highest cena
    }
  }

  onFilterApplied(filtered: boolean): void {
    this.filteredFlag = filtered;
  }


}
