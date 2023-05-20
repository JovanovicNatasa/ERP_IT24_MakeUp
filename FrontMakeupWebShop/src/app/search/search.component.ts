import { Component, OnInit } from '@angular/core';
import { SearchService } from '../search/serach-s.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent {
  constructor(private searchService: SearchService) { }

  onSearch(searchText: string): void {
    // Perform search logic using the searchText value
    console.log('Search Text:', searchText);

    // Pass the search text to the shared service
    this.searchService.setSearchText(searchText);
  }
}
