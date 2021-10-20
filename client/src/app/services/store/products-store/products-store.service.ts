import { Injectable } from '@angular/core';
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

  constructor(private productService: ProductService, private toastr: ToastrService){
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
                this.toastr.error("An unexpected error occurred.")
              });
  }

  async searchProduct(pageNumber, pageSize, sort, idCategories, search) {
    await this.productService.searchProduct(pageNumber, pageSize, sort, idCategories, search)
              .subscribe(res => {
                this.products = res.data;
                this.totalPage = res.totalPage;
              })
  }

  async filterProduct(filterParams) {
    await this.productService.filterProduct(filterParams)
      .subscribe(res => {
        this.products = res.data;
        this.totalPage = res.totalPage;
      })
  }

   add (productObj, idCategories) {
    let result = new Subject<Product>();
    this.productService.add(productObj, idCategories).subscribe(res => {
      result.next(res)
      this.toastr.success("Added successfully", "Product #" + res.id)
    }, (error: AppError) => {
      if(error instanceof BadRequestError)
        return this.toastr.error("Add product failed")
      else throw error
    });
    return result.asObservable();
  }

  getProduct(idProduct) {
    let result = new Subject<any>();
    this.productService.getObject(idProduct).subscribe(res => result.next(res),
    (error:AppError) => {
      if(error instanceof NotFoundError)
        this.toastr.error("Not found")
      else throw error
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
