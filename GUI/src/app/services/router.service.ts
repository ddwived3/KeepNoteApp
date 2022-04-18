import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';

@Injectable()
export class RouterService {

  constructor(private router: Router, private location: Location) { }
  routeToDashboard() {
    // route to dashboard
    this.router.navigate(['dashboard']);
  }
  routeToLogin() {
    // route to login
    this.router.navigate(['login']);
  }

  routeToRegister() {
    // route to register
    this.router.navigate(['register']);
  }

  routeToUser() {
    // route to user registration
    this.router.navigate(['user']);
  }

  routeToEditNoteView(noteId) {
    // route to edit view
    this.router.navigate([
      'dashboard', {
        outlets: {
          noteEditOutlet : ['note', noteId, 'edit']
        }
      }
    ]);
  }

  routeToViewCategory()
  {
    this.router.navigate(['Catview']);
  }

  routeToCreateCategory()
  {
    this.router.navigate(['Catcreate']);
  }

  routeBack() {
    // route back from edit to Read view
    this.location.back();
  }

  routeToNoteView() {
    // route to note view
    this.router.navigate(['dashboard/view/noteview']);
  }

  //  route to list view
  routeToListView() {
    this.router.navigate(['dashboard/view/listview']);
  }
}
