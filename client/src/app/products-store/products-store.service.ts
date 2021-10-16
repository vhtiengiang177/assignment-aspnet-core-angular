import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';
import { AppError } from '../common/app-error';
import { BadRequestError } from '../common/bad-request';
import { NotFoundError } from '../common/not-found-error';
import { Product } from '../model/product.model';
import { ProductService } from '../services/product/product.service';

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

  constructor(private productService: ProductService, private toastr: ToastrService){
  }

  get products(): Product[] {
    return this._products.getValue();
  }

  set products(val: Product[]) {
    this._products.next(val);
  }

  get totalPages(): number {
    return this._totalPage.getValue();
  }

  set totalPages(val: number) {
    this._totalPage.next(val);
  }

  async getAll(pageNumber, pageSize, sort) {
    await this.productService.getAll(pageNumber, pageSize, sort)
              .subscribe(res => {
                this.products = res.data;
                this.totalPages = res.totalPage;
              } ,
              () => {
                this.toastr.error("An unexpected error occurred.")
              });
  }

  async searchProduct(pageNumber, pageSize, sort, idCategories, search) {
    await this.productService.searchProduct(pageNumber, pageSize, sort, idCategories, search)
              .subscribe(res => {
                this.products = res.data;
                this.totalPages = res.totalPage;
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
    // .subscribe(() => {
    //   this.toastr.success("The product has been edited")
    // }, (error) => {
    //   if(error instanceof BadRequestError)
    //     this.toastr.error("Edit product failed")
    //   else throw error
    // })
  }

  delete(id) {
    return this.productService.delete(id)
  }
}
