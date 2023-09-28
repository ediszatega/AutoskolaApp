import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'Autoskola';
  landingPage: boolean;

  ngOnInit(): void {
    if (
      window.location.href == 'http://localhost:4200/' ||
      window.location.href == 'http://localhost:4200/about-us/'
    ) {
      this.landingPage = true;
    }
  }
}
