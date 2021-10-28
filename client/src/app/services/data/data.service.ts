import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { GlobalConstants } from 'src/app/_shared/constants/global-constants';
import { AppError } from 'src/app/_shared/errors/app-error';
import { NotFoundError } from 'src/app/_shared/errors/not-found-error';
import { FilterParamsProduct } from '../model/filter-params-product.model';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(protected routeAPI: string, protected http: HttpClient) { }

  get() {
    return this.http.get<any>(GlobalConstants.apiUrl + this.routeAPI, { 
                      headers: this.authorizationHeader()
                    })
                    .pipe(catchError((error:Response) => {
                      return throwError(new AppError(error));
                    }));
  }

  getObject(id) {
    return this.http.get<any>(GlobalConstants.apiUrl + this.routeAPI + '/' + id, {
      headers: this.authorizationHeader()
    }).pipe(catchError((error:Response) => {
      if(error.status === 404)
        return throwError(new NotFoundError(error))
      return throwError(new AppError(error))
    }))
  }


  authorizationHeader() {
    let headers = new HttpHeaders();
    let token = localStorage.getItem('token');
    
    let jwtHelper = new JwtHelperService();

    if (!jwtHelper.isTokenExpired(token))
    {
      headers = headers.set("Authorization", "Bearer " + token);
    }

    return headers;
  }

}
