import { Observable } from 'rxjs/Observable';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Note } from '../note';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';

import { AuthenticationService } from './authentication.service';

import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

@Injectable()
export class NotesService {

  notes: Array<Note>;
  notesSubject: BehaviorSubject<Array<Note>>;
  token: any;
  username:any;  

  constructor(private http: HttpClient, private authService: AuthenticationService) {
    this.notes = [];
    this.notesSubject = new BehaviorSubject(this.notes);
    this.token = this.authService.getBearerToken();
    this.username = localStorage.getItem('username');
  }

  fetchNotesFromServer() {      
      return this.http.get<Array<Note>>('http://127.0.0.1:5000/api/note/'+this.username, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${this.token}`)
    })
    .subscribe( notes => {
      this.notes = notes;
      this.notesSubject.next(this.notes);
    },
    err => {
      return Observable.throw(err);
    });
  }

  getNotes(): BehaviorSubject<Array<Note>> {
    return this.notesSubject;
  }

  addNote(note: Note): Observable<Note> {
    console.log(note);
    
    return this.http.post<Note>('http://127.0.0.1:5000/api/note', note, {      
      headers : new HttpHeaders()
      .set('Authorization', `Bearer ${this.token}`)
      })
      .do (addNote => {
        this.notes.push(addNote);
        this.notesSubject.next(this.notes);
      })
      .catch(err => {
        return Observable.throw(err);
      });
  }

  editNote(note: Note): Observable<Note> {
    //return this.http.put<Note>(`http://localhost:3000/api/v1/notes/${note.id}`, note, {
    return this.http.put<Note>(`http://127.0.0.1:5000/api/note/${note.id}`, note, {
      headers : new HttpHeaders()
      .set('Authorization', `Bearer ${this.token}`)
    }).do(() => {
      return this.http.get<Note>(`http://127.0.0.1:5000/api/note/${note.id}`, {
        headers: new HttpHeaders().set('Authorization', `Bearer ${this.token}`)
      })
      .subscribe(editNote => {
        console.log(editNote.id);
        console.log(editNote.description);
        
        const noteValue = this.notes.find(notes => notes.id === editNote.id);
        Object.assign(noteValue, editNote);
        this.notesSubject.next(this.notes);
      },
      err => {
        return Observable.throw(err);
      });                  
    });
  }

  deleteNote(note:Note):Observable<any>
  {
    console.log(note.id);    
    return this.http.delete<any>(`http://127.0.0.1:5000/api/note/${note.id}`, {
      headers : new HttpHeaders()
      .set('Authorization', `Bearer ${this.token}`)
    }).do(() => 
    {
      return this.fetchNotesFromServer();
    });
  }

  getNoteById(noteId): Note {
    // Get the note details by passing the noteId
    const noteValue = this.notes.find(note => note.id === noteId);
    return Object.assign({}, noteValue);
  }
}
