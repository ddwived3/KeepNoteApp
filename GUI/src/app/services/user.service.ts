import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/throw';
import { Observable } from 'rxjs/Observable';
import { catchError} from 'rxjs/operators';
import {_throw} from 'rxjs/observable/throw'
import { User } from '../user';
import { AuthenticationService } from './authentication.service';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';

@Injectable()
export class UserService
{
    usersSubject:BehaviorSubject<User>;
    username:any;
    token:any;
    user:User;
    constructor(private httpClient:HttpClient, private authService:AuthenticationService){        
        this.usersSubject = new BehaviorSubject(this.user);
        this.username = this.authService.getUsername();
        this.token = this.authService.getBearerToken();
    }
    private url = 'http://127.0.0.1:';    
    fetchUserProfile()
    {        
        return this.httpClient.get<User>('http://127.0.0.1:55654/api/user/'+this.username, {
        headers: new HttpHeaders().set('Authorization', `Bearer ${this.token}`)
      })
      .subscribe( usr => {
        this.user = usr;
        this.usersSubject.next(this.user);
      },
      err => {
        return Observable.throw(err);
      });
    }

    getUsers(): BehaviorSubject<User> {
        return this.usersSubject;
    }

    registerUser(user:User):Observable<any>{        
        return this.httpClient.post<any>('http://127.0.0.1:55654/api/user', user, {
        headers : new HttpHeaders()
        .set('Authorization', `Bearer ${this.token}`)
      })      
      .catch(err => {
        return Observable.throw(err);
      });
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

    editUser(user:User): Observable<any> {
        return this.httpClient.put<any>(`http://127.0.0.1:55654/api/user/${user.userId}`, user, {
          headers : new HttpHeaders()
          .set('Authorization', `Bearer ${this.token}`)
        })
        .catch(
          err => {
            return Observable.throw(err);
        });                  
    }
}