
import { Component, EventEmitter, Output } from '@angular/core';
import { FilterService } from 'src/app/filter.service';
import { SearchService } from 'src/app/search/serach-s.service';
import { LoginService } from 'src/app/users/login/login.service';


@Component({
  selector: 'app-top-nav',
  templateUrl: './top-nav.component.html',
  styleUrls: ['./top-nav.component.css']
})

export class TopNavComponent {

  @Output() filterApplied: EventEmitter<boolean> = new EventEmitter<boolean>();
  searchText!: string;
  //menuOpened:boolean=false;
  openedMenus: { [key: string]: boolean } = {};

  menuItems: MenuItem[] = [
    { name: 'Golden Rose', selected: false },
    { name: 'MaxFactor', selected: false },
    { name: 'Essence', selected: false },
    { name: 'Revolution', selected: false },
    { name: 'Aura', selected: false }
  ];

  categoryItems: MenuItem[] = [
    { name: 'Olovke za usne', selected: false },
    { name: 'Olovke za obrve', selected: false },
    { name: 'Ajlajneri i olovke za oci', selected: false },
    { name: 'Maskare', selected: false },
    { name: 'Ruzevi za usne', selected: false }
  ];


  constructor(private searchService: SearchService,private filterService: FilterService,
    public loginService: LoginService ) { }

    isLoggedIn(): boolean {
      return this.loginService.getLoggedIn();
    }

    getUsername(): string {
      return this.loginService.getUsername();
    }

  ngOnInit(): void {
    this.searchService.searchText$.subscribe((searchText: string) => {
      // Handle the search text change
      this.searchText = searchText;
      console.log('Search Text:', this.searchText);
    });
  }
  toggleMenu(menuId: string): void {
    this.openedMenus[menuId] = !this.openedMenus[menuId];
  }

  isMenuOpened(menuId: string): boolean {
    return this.openedMenus[menuId] || false;
  }

  clickedOutside(menuId: string): void {
    this.openedMenus[menuId] = false;
  }
  onSearch(): void {
    // Emit the search text to the search service
    this.searchService.setSearchText(this.searchText);
  }

  filterByPurpose(purpose: string): void {
    this.searchService.filterProductsByPurpose(purpose);
    this.filterApplied.emit(true);
  }

  clearFilters(): void {
    this.searchText = ''; // Clear the search text
    this.searchService.setSearchText(''); // Emit an empty search text to clear the filters
    this.filterApplied.emit(false);
  }

  applyFilters(): void {
    const selectedBrands = this.menuItems
      .filter(item => item.selected)
      .map(item => item.name);

    this.filterService.setFilter(selectedBrands);
    this.filterApplied.emit(true);
  }

  applyTypeFilters(): void {
    const selectedTypes = this.categoryItems
      .filter(item => item.selected)
      .map(item => item.name);

    this.filterService.setTypeFilter(selectedTypes);
    this.filterApplied.emit(true);
  }
}


interface MenuItem {
  name: string;
  selected: boolean;
}
