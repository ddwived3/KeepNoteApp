import { NotesService } from './../services/notes.service';
import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Note } from '../note';
import { CategoryService } from '../services/category.service';
import { Category } from '../category';

@Component({
  selector: 'app-edit-note-view',
  templateUrl: './edit-note-view.component.html',
  styleUrls: ['./edit-note-view.component.css']
})
export class EditNoteViewComponent implements OnInit {
  note: Note;
  errMessage: string;
  categories: Array<Category> = [];
  
  constructor(private dialogRef: MatDialogRef<EditNoteViewComponent>,
              @Inject(MAT_DIALOG_DATA) private data: any,
              private notesService: NotesService, private categoryService:CategoryService) { }
  
  onChange(selCategory){    
    this.note.category = selCategory;
  }
  ngOnInit() {
    // While opening the edit dialog get the note details by passing the noteId
    //this.categoryService.fetchCategoriesFromServer();
    this.categoryService.getCategories().subscribe(response=> 
      {        
        Object.assign(this.categories, response);                
      },
      err => {
        // remove the added note if there is any error
        this.categories.pop();
        this.errMessage = err.message;
      });

    this.note = this.notesService.getNoteById(this.data.noteId);
    console.log(this.note);
    
  }

  onSave() {
    console.log(this.note);
    
    // Close the edit dialog while clicking on save
    this.notesService.editNote(this.note).subscribe(editNote => {
      this.dialogRef.close();
    },
    err => {
      this.errMessage = err.message;
    });
  }
}
