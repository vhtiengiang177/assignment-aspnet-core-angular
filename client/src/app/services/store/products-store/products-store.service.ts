import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject, Subject } from 'rxjs';
import { AppError } from 'src/app/_shared/errors/app-error';
import { BadRequestError } from 'src/app/_shared/errors/bad-request';
import { NotFoundError } from 'src/app/_shared/errors/not-found-error';
import { FilterParamsProduct } from '../../model/filter-params-product.model';
import { Product } from '../../model/product.model';
import { ProductService } from '../../product/product.service';

@Injectable({
  providedIn: 'root'
})
export class ProductsStoreService {
  // store
  private readonly _products = new BehaviorSubject<Product[]>([]);

  // observable 
  readonly products$ = this._products.asObservable();

  private readonly _totalPage = new BehaviorSubject<number>(0);
  readonly totalPage$ = this._totalPage.asObservable();

  constructor(private productService: ProductService, 
    private toastr: ToastrService,
    private router: Router){
    if(this.products.length == 0) {
      let filter: FilterParamsProduct = {};
      this.getAll(filter);
    }
  }

  get products(): Product[] {
    return this._products.getValue();
  }

  set products(val: Product[]) {
    this._products.next(val);
  }

  get totalPage(): number {
    return this._totalPage.getValue();
  }

  set totalPage(val: number) {
    this._totalPage.next(val);
  }

  async getAll(filterParams: FilterParamsProduct) {
    await this.productService.getAll(filterParams)
              .subscribe(res => {
                this.products = res.data;
                this.totalPage = res.totalPage;
              } ,
              () => {
                this.toastr.error("An unexpected error occurred.", "List Products")
              });
  }

   add (productObj, idCategories) {
    let result = new Subject<Product>();
    this.productService.add(productObj, idCategories).subscribe(res => {
      result.next(res)
      this.toastr.success("Added successfully", "Product #" + res.id)
    }, (error: AppError) => {
      if(error instanceof BadRequestError)
        return this.toastr.error("Add product failed")
      else this.toastr.error("An unexpected error occurred.", "Add Product")
    });
    return result.asObservable();
  }

  getProduct(idProduct) {
    let result = new Subject<any>();
    this.productService.getObject(idProduct).subscribe(res => result.next(res),
    (error:AppError) => {
      if(error instanceof NotFoundError) {
        this.router.navigate(['/not-found'])
      }
    })
    return result.asObservable();
  }

  edit(product, idCategories) {
    return this.productService.edit(product, idCategories)
  }

  delete(id) {
    return this.productService.delete(id)
  }
}
