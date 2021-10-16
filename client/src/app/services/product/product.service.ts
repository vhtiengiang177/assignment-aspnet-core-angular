import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AppError } from 'src/app/common/app-error';
import { BadRequestError } from 'src/app/common/bad-request';
import { GlobalConstants } from 'src/app/common/global-constants';
import { NotFoundError } from 'src/app/common/not-found-error';
import { Product } from 'src/app/model/product.model';
import { DataService } from '../data/data.service';
@Injectable({
  providedIn: 'root'
})
export class ProductService extends DataService {
  
  constructor(http: HttpClient) {
    super('/products', http);
  }

  searchProduct(pageNumber, pageSize, sort, idCategories, search) {
    let options = {
      pageNumber,
      pageSize,
      sort,
      idCategories,
      search
    }
    return this.http.get<any>(GlobalConstants.apiUrl + this.routeAPI + '/searchproduct', { 
                      headers: this.authorizationHeader(),
                      params: options
                    })
                    .pipe(catchError((error:Response) => {
                      return Observable.throw(new AppError(error));
                    }));

  }

  add(product, idCategories) {
    let options = {
      idCategories
    }

    return this.http.post<Product>(GlobalConstants.apiUrl + this.routeAPI, product, { 
                      headers: this.authorizationHeader(),
                      params: options
                    })
                    .pipe(catchError((error: Response) => {
                      if(error.status == 400) {
                        return Observable.throw(new BadRequestError())
                      }
                      return Observable.throw(new AppError(error))
                    }))
  }

  edit(product, idCategories) {
    let options = {
      idCategories
    }

    return this.http.put<Product>(GlobalConstants.apiUrl + this.routeAPI + "/" + product.id, product, {
      headers: this.authorizationHeader(),
      params: options
    }).pipe(catchError((error: Response) => {
      if(error.status == 400) {
        return throwError(new BadRequestError())
      }
      else throwError(new AppError(error))
    }))
  }

  delete(id) {
    let options = {
      id
    }

    return this.http.delete(GlobalConstants.apiUrl + this.routeAPI +"/" + id, {
      headers: this.authorizationHeader(),
      params: options
    }).pipe(catchError((error: Response) => {
      if(error.status == 404)
        return throwError(new NotFoundError())
      else if(error.status == 400)
        return throwError(new BadRequestError())
      else throwError(new AppError(error))
    }))
  }

}
