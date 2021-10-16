import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject } from 'rxjs';
import { Category } from '../model/category.model';
import { CategoryService } from '../services/category/category.service';

@Injectable({
  providedIn: 'root'
})
export class CategoriesStoreService {

  private readonly _categories = new BehaviorSubject<Category[]>([]);

  readonly categories$ = this._categories.asObservable();

  constructor(private categoryService: CategoryService, private toastr:ToastrService) {
    this.getAll(null,null,null)
   }

  get categories() : Category[] {
    return this._categories.value;
  }

  set categories(val:Category[]) {
    this._categories.next(val);
  }

  async getAll(pageNumber, pageSize, sort){
    await this.categoryService.getAll(pageNumber, pageSize, sort)
            .subscribe(res => this.categories = res,
              () => {
                this.toastr.error("An unexpected error occurred.")
              });
  }
}
