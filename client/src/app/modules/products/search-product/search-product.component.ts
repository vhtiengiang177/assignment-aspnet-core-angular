import { Component, Input, OnInit, Output, ViewChild, EventEmitter } from '@angular/core';
import { MatOption, MatSelect } from '@angular/material';
import { CategoriesStoreService } from 'src/app/services/store/categories-store/categories-store.service';
import { Category } from 'src/app/services/model/category.model';
import { SearchAdvance } from 'src/app/services/model/search-advance.model';
import { faSearch, faChevronCircleDown, faChevronCircleUp, faSync } from '@fortawesome/free-solid-svg-icons';
import { FilterParamsProduct } from 'src/app/services/model/filter-params-product.model';

@Component({
  selector: 'search-product',
  templateUrl: './search-product.component.html',
  styleUrls: ['./search-product.component.css']
})
export class SearchProductComponent implements OnInit {
  @ViewChild('selectCategory', {static: false}) selectCategory: MatSelect;
  @ViewChild('selectRating', {static:false}) selectRating: MatSelect;
  @Input('post-per-page') postPerPage : number;
  @Output('search-event') searchEvent = new EventEmitter<FilterParamsProduct>();
  @Output('reset-event') resetEvent = new EventEmitter();

  isAdvance: boolean = false;
  isAllCategories : boolean = false;
  isAllRating: boolean = false;
  isSearchInputNull : boolean;
  search: string;
  categories: Category[] = [];
  ratingOption: number[] = [];
  rating: number[] = [];
  minPrice: number;
  maxPrice: number;
  fasearch = faSearch;
  facircledown = faChevronCircleDown;
  facircleup = faChevronCircleUp;
  faSync = faSync;

  constructor(private categoriesStore: CategoriesStoreService) { 
    this.ratingOption = [1,2,3,4,5]
    
  }

  ngOnInit() {
  }

  searchFilter(){
    this.isAdvance = !this.isAdvance;
  }

  selectedCategories(selected){
    if(this.isAllCategories) {
      this.selectCategory.options.first.deselect();
      this.isAllCategories = !this.isAllCategories; 
    }
    else if (this.selectCategory.options.length == selected.length + 1) {
      this.selectCategory.options.first.select();
      this.isAllCategories = !this.isAllCategories; 
    }
    else
      this.categories = selected;
  }

  selectedAllCategories() {
    this.isAllCategories = !this.isAllCategories; 
    if(this.isAllCategories) {
      this.selectCategory.options.forEach((item : MatOption) => item.select());
    }
    else {
      this.selectCategory.options.forEach((item : MatOption) => {item.deselect()});
    }
    this.selectCategory.close();
  }

  selectedRating(selected) {
    if(this.isAllRating) {
      this.selectRating.options.first.deselect();
      this.isAllRating = !this.isAllRating; 
    }
    else if (this.selectRating.options.length == selected.length + 1) {
      this.selectRating.options.first.select();
      this.isAllRating = !this.isAllRating; 
    }
    else
      this.rating = selected;
  }

  selectedAllRating() {
    this.isAllRating = !this.isAllRating; 
    if(this.isAllRating) {
      this.selectRating.options.forEach((item : MatOption) => item.select());
    }
    else {
      this.selectRating.options.forEach((item : MatOption) => {item.deselect()});
    }
    
    console.log('selected ' + this.rating)
    this.selectRating.close();
  }

  checkMinPrice() {
    if(this.minPrice < 0) {
      this.minPrice = 0;
    }
    if(this.maxPrice) {
      if(this.minPrice > this.maxPrice) {
        this.minPrice = this.maxPrice;
      }
    }
  }
  
  checkMaxPrice() {
    if(this.minPrice) {
      if(this.maxPrice < this.minPrice) {
        this.maxPrice = this.minPrice;
      }
    }
  }

  searchProduct(searchInput) {
    let filterParams: FilterParamsProduct = {};
    
    if(this.isAdvance) {
      let idCategories : number[] = [];
      this.categories.filter(k=>k.id != 0).forEach(element => {
        idCategories.push(element.id)
      });
      
      filterParams = {
        idcategories: idCategories,
        rating: this.rating.filter(r => r != 0),
        minprice: this.minPrice,
        maxprice: this.maxPrice,
        content: searchInput
      }
      this.searchEvent.emit(filterParams);
    }
    else if(searchInput) {
      filterParams = {
        content: searchInput
      }
      this.searchEvent.emit(filterParams);
    }
  }

  reset() {
    this.categories = [];
    this.rating = [];
    this.minPrice = null;
    this.maxPrice = null;

    this.resetEvent.emit();
  }

}
