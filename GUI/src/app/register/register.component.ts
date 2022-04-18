import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { RouterService } from '../services/router.service';
import { AuthenticationService } from '../services/authentication.service';
import { Register } from '../register';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  submitMessage:any;
  username = new FormControl('',[Validators.required]);
  password = new FormControl('',[Validators.required]);
  confirmPassword = new FormControl('',[Validators.required]);
  
  constructor(private routerService:RouterService, private authService:AuthenticationService) { }

  registrationSubmit(){
    const user = new Register(this.username.value, this.password.value);
    
    if(this.username.valid && 
      this.password.valid
      ){
        this.authService.addUserForAuthentication(user).subscribe(
          () =>{
            this.routerService.routeToLogin();
          },
          err=>{
            this.submitMessage = err.status == 403 ? err.error.message:err.message;
          });        
      }    
  }
  ngOnInit() {
  }

  getUserNameErrorMessage() {
    // Required error message for username
    return this.username.hasError('required') ? 'You must enter a Username' : '';
  }
  getPasswordErrorMessage() {
    // Required error message for password
    return this.password.hasError('required') ? 'You must enter a password' : '';
  }
  getConfirmPasswordErrorMessage() {
    var message:any;
    if(this.password.value != this.confirmPassword.value)
      message = this.confirmPassword.hasError('required') ? 'Password and Confirm Password do not match' : '';
    else
      // Required error message for confirm password
      message = this.confirmPassword.hasError('required') ? 'You must enter a password' : '';

    return message;
  }
}