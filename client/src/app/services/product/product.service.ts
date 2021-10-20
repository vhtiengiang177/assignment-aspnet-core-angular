import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { GlobalConstants } from 'src/app/_shared/constants/global-constants';
import { Product } from 'src/app/services/model/product.model';
import { DataService } from '../data/data.service';
import { BadRequestError } from 'src/app/_shared/errors/bad-request';
import { AppError } from 'src/app/_shared/errors/app-error';
import { NotFoundError } from 'src/app/_shared/errors/not-found-error';
import { FilterParamsProduct } from '../model/filter-params-product.model';
import { ResponseList } from '../model/response-list.model';
@Injectable({
  providedIn: 'root'
})
export class ProductService extends DataService {
  
  constructor(http: HttpClient) {
    super('/products', http);
  }

  
  getAll (filterParams) {
    return this.http.get<any>(GlobalConstants.apiUrl + this.routeAPI + this.convertToQueryString(filterParams), { 
                      headers: this.authorizationHeader()
                    })
                    .pipe(catchError((error:Response) => {
                      return throwError(new AppError(error));
                    }));
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

  filterProduct(filterParams) {
    return this.http.get<ResponseList>(GlobalConstants.apiUrl + this.routeAPI + '/filterproduct' + this.convertToQueryString(filterParams), { 
      headers: this.authorizationHeader()
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

  convertToQueryString(filterParams: FilterParamsProduct): string {
    const cloneParams = { ...filterParams };
    let query = '?';
  
    if (cloneParams.idcategories) {
      cloneParams.idcategories.forEach((categoryId) => {
        query += `idCategories=${categoryId}&`;
      });
    }
    delete cloneParams.idcategories;

    if (cloneParams.rating) {
      cloneParams.rating.forEach((item) => {
        query += `rating=${item}&`;
      });
    }
    delete cloneParams.rating;
  
    Object.entries(cloneParams).forEach(([key, value]) => {
      if (value != undefined) {
        query += `${key}=${value}&`;
      }
    });

    return query;
  }

}
