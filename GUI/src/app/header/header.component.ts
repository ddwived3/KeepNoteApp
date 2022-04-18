import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

import { RouterService } from './../services/router.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  isNoteView:boolean;

  constructor(private routerService: RouterService, private location: Location) {}

  ngOnInit() {
    this.isNoteView = (location.pathname.includes('/view/noteview')) ? true :
                      (location.pathname.includes('/view/listview')) ? false : true;
  }

  showListView() {
    this.isNoteView = false;
    this.routerService.routeToListView();
  }
  showNoteView() {
    this.isNoteView = true;
    this.routerService.routeToNoteView();
  }
}
