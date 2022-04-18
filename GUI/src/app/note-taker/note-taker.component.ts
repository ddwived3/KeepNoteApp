import { Component } from '@angular/core';
import { Note } from './../note';
import { Category } from './../category';
import { NotesService } from './../services/notes.service';
import { CategoryService } from '../services/category.service';

@Component({
  selector: 'app-note-taker',
  templateUrl: './note-taker.component.html',
  styleUrls: ['./note-taker.component.css']
})
export class NoteTakerComponent {
  errMessage: string;
  note: Note = new Note();
  notes: Array<Note> = [];
  categories: Array<Category> = [];

  constructor(private notesService: NotesService, private categoryService:CategoryService) {}

  ngOnInit()
  {
    this.categoryService.fetchCategoriesFromServer();
    this.categoryService.getCategories().subscribe(response=> 
      {        
        Object.assign(this.categories, response);                
      },
      err => {
        // remove the added note if there is any error
        this.categories.pop();
        this.errMessage = err.message;
      });
  }

  // Calls a service and saves the note to db.json file
   insertNotes() {
    const title = this.note.title.trim();
    const text = this.note.description.trim();
    //const category = this.note.category.trim();
    console.log(this.note);
    
    this.note.createdBy = localStorage.getItem('username');
    if (title === '' || text === ''){//} || category == '') {
      // add the error message when fields are empty
      this.errMessage = 'Title, Text and Category are required fields';
    } else {
      // add notes to service
      //this.notes.push(this.note);
      this.notesService.addNote(this.note).subscribe(
        () => {
          this.errMessage = '';
        },
        err => {
          // remove the added note if there is any error
          this.notes.pop();
          this.errMessage = err.message;
      });      
    }
    this.note = new Note();
  }
}
