import { Component } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent {
  sideMenuOpened = false;
  closedSideMenu = true;
  constructor() {}

  toggleSideMenu() {
    this.sideMenuOpened = !this.sideMenuOpened;
    this.closedSideMenu = !this.closedSideMenu;
  }

  navLinkClicked() {
    this.closedSideMenu = true;
    this.sideMenuOpened = !this.sideMenuOpened;
  }
}
