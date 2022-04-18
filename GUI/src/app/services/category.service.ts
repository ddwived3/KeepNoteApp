import { Observable } from 'rxjs/Observable';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Category } from '../category';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';

import { AuthenticationService } from './authentication.service';

import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

@Injectable()
export class CategoryService {

  categories: Array<Category>;
  categoriesSubject: BehaviorSubject<Array<Category>>;
  category:Category;
  token: any;
  username:any;  

  constructor(private httpClient: HttpClient, private authService: AuthenticationService) {
    this.categories = [];
    this.categoriesSubject = new BehaviorSubject(this.categories);
    this.token = this.authService.getBearerToken();
    this.username = localStorage.getItem('username');
  }

  fetchCategoriesFromServer() {      
      return this.httpClient.get<Array<Category>>('http://127.0.0.1:55776/api/category/'+this.username, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${this.token}`)
    })
    .subscribe( response => {
      console.log(response);      
      Object.assign(this.categories, response);      
      this.categoriesSubject.next(this.categories);
    },
    err => {
      return Observable.throw(err);
    });
  }

  registerCategory(category:Category):Observable<Category>{        
      return this.httpClient.post<Category>('http://127.0.0.1:55776/api/category', category, {
      headers : new HttpHeaders().set('Authorization', `Bearer ${this.token}`)
    })
    .catch(err => {
      return Observable.throw(err);
    });
  }
  getCategories(): BehaviorSubject<Array<Category>> {
    return this.categoriesSubject;
  }

  editCategory(category:any): Observable<any> {    
    return this.httpClient.put<any>(`http://127.0.0.1:55776/api/category/${category.id}`, category, {
      headers : new HttpHeaders()
      .set('Authorization', `Bearer ${this.token}`)
    }).catch(
      err => {
        return Observable.throw(err);                    
    });
  }

  deleteCategory(categoryId:any): Observable<any> {    
    return this.httpClient.delete<any>(`http://127.0.0.1:55776/api/category/${categoryId}`, {
      headers : new HttpHeaders()
      .set('Authorization', `Bearer ${this.token}`)
    }).catch(
      err => {
        return Observable.throw(err);                    
    });
  }
}
