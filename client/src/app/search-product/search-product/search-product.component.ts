import { Component, Input, OnInit, Output, ViewChild, EventEmitter } from '@angular/core';
import { MatOption, MatSelect } from '@angular/material';
import { CategoriesStoreService } from 'src/app/categories-store/categories-store.service';
import { Category } from 'src/app/model/category.model';
import { SearchAdvance } from 'src/app/model/search-advance.model';
import { faSearch, faChevronCircleDown, faChevronCircleUp } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'search-product',
  templateUrl: './search-product.component.html',
  styleUrls: ['./search-product.component.css']
})
export class SearchProductComponent implements OnInit {
  @ViewChild('selectCategory', {static: false}) selectCategory: MatSelect;
  @Input('post-per-page') postPerPage : number;
  @Output('search-event') searchEvent = new EventEmitter<SearchAdvance>();

  isAdvance: boolean = false;
  isAll : boolean = false;
  isSearchInputNull : boolean;
  categories: Category[] = [];
  fasearch = faSearch;
  facircledown = faChevronCircleDown;
  facircleup = faChevronCircleUp;

  constructor(private categoriesStore: CategoriesStoreService) { 
    
  }

  ngOnInit() {
  }

  searchFilter(){
    this.isAdvance = !this.isAdvance;
  }

  selectedCategories(selected){
    if(this.isAll) {
      this.selectCategory.options.first.deselect();
      this.isAll = !this.isAll; 
    }
    else if (this.selectCategory.options.length == selected.length + 1) {
      this.selectCategory.options.first.select();
      this.isAll = !this.isAll; 
    }
    else
      this.categories = selected;
  }

  selectedAll(){
    this.isAll = !this.isAll; 
    if(this.isAll) {
      this.selectCategory.options.forEach((item : MatOption) => item.select());
    }
    else {
      this.selectCategory.options.forEach((item : MatOption) => {item.deselect()});
    }
    this.selectCategory.close();
  }

  searchProduct(searchInput) {
    let searchAdvanceObj: SearchAdvance = {search: '', idCategories: []};
    searchAdvanceObj.search = searchInput;
    if(this.isAdvance) {
      let idCategories : number[] = [];
      this.categories.filter(k=>k.id != 0).forEach(element => {
        idCategories.push(element.id)
      });
      searchAdvanceObj.idCategories = idCategories;
    }
    this.searchEvent.emit(searchAdvanceObj);
  }

}
