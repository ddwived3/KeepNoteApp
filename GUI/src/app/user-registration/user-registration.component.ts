import { Component, OnInit } from '@angular/core';
import { RouterService } from './../services/router.service';
import { User } from './../user';
import { FormControl, Validators } from '@angular/forms';
import { UserService } from '../services/user.service';
import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-user-registration',
  templateUrl: './user-registration.component.html',
  styleUrls: ['./user-registration.component.css']
})
export class UserRegistrationComponent implements OnInit {
  submitMessage:any;
  username:string;
  name = new FormControl('',[Validators.required]);
  contact = new FormControl('',[Validators.required]);
  user:User;
  constructor(private userService:UserService, private routerService:RouterService, private authService:AuthenticationService) { 
    this.username = this.authService.getUsername();
  }

  registrationSubmit(){
    this.user = new User(this.username,this.name.value, this.contact.value, new Date());
    
    if(this.name.valid && this.contact.valid)
      {
        this.userService.registerUser(this.user).subscribe(
          ()=>{
            this.routerService.routeToDashboard();
          },
          err=>{
            this.submitMessage = err.status == 403 ? err.error.message:err.message;
          });
      }    
  }
  ngOnInit() {    
    // get the user while loading the page
    this.userService.fetchUserProfile();    
    this.userService.getUsers().subscribe(
      response => {
        var usr = new User(this.username);
        Object.assign(usr, response);                
        this.name.setValue(usr.name);
        this.contact.setValue(usr.contact);        
      });      
  }

  getNameErrorMessage() {
    // Required error message for username
    return this.name.hasError('required') ? 'You must enter a Name' : '';
  }
  getContactErrorMessage() {
    // Required error message for contact
    return this.contact.hasError('required') ? 'You must enter a Contact' : '';
  }
}