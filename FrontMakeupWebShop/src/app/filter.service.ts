import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FilterService {
  private brandFilterSubject: Subject<string[]> = new Subject<string[]>();
  private typeFilterSubject: Subject<string[]> = new Subject<string[]>();

  brandFilter$ = this.brandFilterSubject.asObservable();
  typeFilter$ = this.typeFilterSubject.asObservable();

  setFilter(brands: string[]): void {
    this.brandFilterSubject.next(brands);
  }

  setTypeFilter(types: string[]): void {
    this.typeFilterSubject.next(types);
  }
}
