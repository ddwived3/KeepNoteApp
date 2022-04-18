import { Component, OnInit } from '@angular/core';
import { Note } from '../note';
import { NotesService } from './../services/notes.service';

@Component({
  selector: 'app-list-view',
  templateUrl: './list-view.component.html',
  styleUrls: ['./list-view.component.css']
})
export class ListViewComponent implements OnInit {

  notStartedNotes: Array<Note>;
  startedNotes: Array<Note>;
  completedNotes: Array<Note>;
  notes:Array<Note>;
  constructor(private notesService: NotesService) {}

  ngOnInit() {
    // get the notes while loading the page
    this.notesService.getNotes().subscribe(
      data => {
        // not started notes
        // this.notStartedNotes = response.filter(function(note){
        //   return note.state === 'not-started';
        // });
        // // Started notes
        // this.startedNotes = response.filter(function(note){
        //   return note.state === 'started';
        // });
        // // Completed notes
        // this.completedNotes = response.filter(function(note){
        //   return note.state === 'completed';
        // });
      
        this.notes = data
      }
    );      
  }
}
