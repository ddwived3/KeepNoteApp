import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../services/category.service';
import { Category } from '../category';

@Component({
  selector: 'app-view-category',
  templateUrl: './view-category.component.html',
  styleUrls: ['./view-category.component.css']
})
export class ViewCategoryComponent implements OnInit {
  categories:Array<Category>;
  constructor(private catService:CategoryService) { }

  ngOnInit() {
    this.catService.fetchCategoriesFromServer();    
    this.catService.getCategories().subscribe(
      response => {        
        Object.assign(this.categories, response);        
    });            
  }
}
