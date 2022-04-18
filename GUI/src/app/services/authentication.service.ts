import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/throw';
import { Observable } from 'rxjs/Observable';
import { catchError} from 'rxjs/operators';
import {_throw} from 'rxjs/observable/throw'

@Injectable()
export class AuthenticationService {
  private auth_url = 'http://127.0.0.1:8089/';//'http://localhost:3000/';

  constructor(private httpClient: HttpClient) {}

  authenticateUser(emp):Observable<any>{
    var body = JSON.stringify(emp);
    //const params = new HttpParams().set('ID', 'hello');  
    console.log(body);    
    const headers = new HttpHeaders().set('content-type', 'application/json');  
    console.log(headers);    
    return this.httpClient.post(`${this.auth_url}auth/login`,body, {headers}).pipe(catchError(this.handleError));
  }

  addUserForAuthentication(user):Observable<any>
  {
    var body = JSON.stringify(user);
    console.log(body);    
    const headers = new HttpHeaders().set('content-type', 'application/json');  
    console.log(headers);    
    return this.httpClient.post('http://127.0.0.1:8089/auth/register', body, {headers}).pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {  
    if (error.error instanceof ErrorEvent) {  
        // A client-side or network error occurred. Handle it accordingly.    
        console.error('An error occurred:', error.error.message);  
    } else {  
        // the backend returned an unsuccessful response code.    
        // the response body may contain clues as to what went wrong,    
        console.error(`Backend returned code ${error.status}, ` + `body was: ${error.error}`);  
    }  
    // return an observable with a user-facing error message    
    return _throw('Something bad happened; please try again later.');  
}  

  // to check the user authentication
 /* authenticateUser(data) {
    //return this.httpClient.post(`${this.auth_url}auth/v1/`, data);
    console.log(data.password);
    
    return this.httpClient.post(`${this.auth_url}auth/login/`, JSON.stringify(data), this.httpOptions);
  }*/
// set the token to localstorage
  setBearerToken(token) {
    console.log(token);
    localStorage.setItem('bearerToken', token);
  }
  
  getUsername()
  {
    return localStorage.getItem('username');
  }

// Get the token from the local storage
  getBearerToken() {
    return localStorage.getItem('bearerToken');
  }
  // to validate if the user is authenticated or not
  isUserAuthenticated(token): Promise<boolean> {
    return Promise.resolve(token!= '');
  }
}
