import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DataService } from '../data/data.service';

@Injectable({
  providedIn: 'root'
})
export class CategoryService extends DataService {

  constructor(http:HttpClient) { 
    super('/categories', http);
  }
}
