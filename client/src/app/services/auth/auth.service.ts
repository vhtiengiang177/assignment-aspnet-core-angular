import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { JwtHelperService } from "@auth0/angular-jwt";
import { GlobalConstants } from 'src/app/_shared/constants/global-constants';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { 
  }

  login(account) {
    return this.http.post(GlobalConstants.apiUrl + '/token', account, { responseType: 'text'})
      .pipe(map(res => {
        if(res){
          localStorage.setItem('token', res);
          return true;
        }
        return false;
      }));
  }

  logout(){
    localStorage.removeItem('token');
  }

  isLoggedIn() {
    let jwtHelper = new JwtHelperService();
    let token = localStorage.getItem('token');
    if(!token)
      return false;
    return !jwtHelper.isTokenExpired(token);
  }

  getCurrentUser(){
    let jwtHelper = new JwtHelperService();
    let token = localStorage.getItem('token');
    
    return jwtHelper.decodeToken(token);
  }
}
