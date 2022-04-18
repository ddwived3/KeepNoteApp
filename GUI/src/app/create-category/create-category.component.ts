import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { Category } from '../category';
import { CategoryService } from '../services/category.service';
import { AuthenticationService } from '../services/authentication.service';
import { RouterService } from '../services/router.service';

@Component({
  selector: 'app-create-category',
  templateUrl: './create-category.component.html',
  styleUrls: ['./create-category.component.css']
})
export class CreateCategoryComponent implements OnInit {
  submitMessage:any;
  username:string;
  category:Category;
  name = new FormControl('',[Validators.required]);
  isPublic = new FormControl('',[Validators.required]);

  constructor(
    private catService:CategoryService, 
    private authService:AuthenticationService,
    private routerService:RouterService
    ) {
    this.username = this.authService.getUsername();
  }

  categorySubmit(){
    this.category = new Category(this.name.value, this.isPublic.value, this.username);    
    if(this.name.valid)
      {
        this.catService.registerCategory(this.category).subscribe(
          ()=>{
            this.routerService.routeToDashboard();
          },
          err=>{
            this.submitMessage = err.status == 403 ? err.error.message:err.message;
          });
      }    
  }

  ngOnInit() {
    /*this.catService.fetchUserProfile();    
    this.catService.getUsers().subscribe(
      response => {
        var category = new Category();        
        Object.assign(category, response);                
        this.name.setValue(category.name);
        this.contact.setValue(category.isPublic);        
      });      
      */
  }

  getNameErrorMessage() {
    // Required error message for username
    return this.name.hasError('required') ? 'You must enter a Name' : '';
  }
}
