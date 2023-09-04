import { Component } from '@angular/core';
import { Lecturer } from 'src/app/models/lecturer';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-lecturers',
  templateUrl: './lecturers.component.html',
  styleUrls: ['./lecturers.component.css'],
})
export class LecturersComponent {
  allLecturers: any[] = [];
  searchLecturers: string = '';

  selectedLecturer: Lecturer;
  pageLecturer = 1;

  showAddPopup: boolean;
  showEditPopup: boolean;

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.fetchData();
    this.showAddPopup = false;
    this.showEditPopup = false;
  }

  public fetchData() {
    this.userService.getLecturers().subscribe((lecturers) => {
      this.allLecturers = lecturers;
    });
  }

  getLecturers() {
    if (this.allLecturers == null) return [];
    return this.allLecturers.filter((x: any) =>
      x.firstName.toLowerCase().startsWith(this.searchLecturers.toLowerCase())
    );
  }

  addLecturer() {
    this.showAddPopup = true;
  }
  editLecturer(lecturer: Lecturer) {
    this.selectedLecturer = lecturer;
    this.showEditPopup = true;
  }

  addPopupClosed() {
    this.showAddPopup = false;
  }

  editPopupClosed() {
    this.showEditPopup = false;
  }

  changePage(event) {
    this.pageLecturer = event;
  }
}
