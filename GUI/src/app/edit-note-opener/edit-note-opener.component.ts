import { Component } from '@angular/core';
import { MatDialog } from '@angular/material';
import { EditNoteViewComponent } from './../edit-note-view/edit-note-view.component';
import { RouterService } from '../services/router.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-edit-note-opener',
  templateUrl: './edit-note-opener.component.html',
  styleUrls: ['./edit-note-opener.component.css']
})
export class EditNoteOpenerComponent {
  constructor(private routerService: RouterService, private dialog: MatDialog,
              private activatedRoute: ActivatedRoute) {
    const noteId = +this.activatedRoute.snapshot.paramMap.get('noteId');
    // Open the Edit dialog while clicking on note card by passing noteId
    this.dialog.open(EditNoteViewComponent, {
      data: {
        noteId: noteId
      }
    })
    .afterClosed().subscribe(
      response => {
        // Route back to note/list view after editing
        this.routerService.routeBack();
      }
    );
  }

}
