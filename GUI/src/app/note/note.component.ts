import { Component, Input } from '@angular/core';
import { Note } from './../note';
import { RouterService } from './../services/router.service';
import { NotesService } from '../services/notes.service';

@Component({
  selector: 'app-note',
  templateUrl: './note.component.html',
  styleUrls: ['./note.component.css']
})
export class NoteComponent {

  @Input()
  note: Note;
  notes: Array<Note> = [];
  constructor(private routerService: RouterService, private noteService:NotesService) {}

  deleteView()
  {         
    this.noteService.deleteNote(this.note).subscribe(()=>{      
    });
  }

  openEditView() {
    // Route to the edit view while clicking on note
    this.routerService.routeToEditNoteView(this.note.id);
  }
}
