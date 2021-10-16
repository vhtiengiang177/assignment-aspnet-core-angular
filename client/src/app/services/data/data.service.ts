import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AppError } from 'src/app/common/app-error';
import { BadRequestError } from 'src/app/common/bad-request';
import { GlobalConstants } from 'src/app/common/global-constants';
import { NotFoundError } from 'src/app/common/not-found-error';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(protected routeAPI: string, protected http: HttpClient) { }

  getAll(pageNumber, pageSize, sort) {
    let params = new HttpParams();

    if(pageNumber != null)
      params = params.set('pageNumber', pageNumber);
    if(pageSize != null)
      params = params.set('pageSize', pageSize);
    if(sort != null)
      params = params.set("sort", sort);

    return this.http.get<any>(GlobalConstants.apiUrl + this.routeAPI, { 
                      headers: this.authorizationHeader(), 
                      params: params 
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

  handleError(error: HttpErrorResponse) {
    // let errorMessage = '';
 
    // if (error.error instanceof ErrorEvent) {
    //   // client-side error
    //   errorMessage = error.error.message;
    // } else {
    //   // server-side error
    //   errorMessage = `Status error code: ${error.status} \nMessage: ${error.message} `;
    // }
    // window.alert(errorMessage);
 
    // return throwError(errorMessage);
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
