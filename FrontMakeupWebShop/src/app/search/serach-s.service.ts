import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SearchService {
  private searchTextSubject: Subject<string> = new Subject<string>();

  searchText$ = this.searchTextSubject.asObservable();

  setSearchText(searchText: string): void {
    this.searchTextSubject.next(searchText);
  }

  filterProductsByPurpose(purpose: string): void {
    this.searchTextSubject.next(purpose);
  }

}
