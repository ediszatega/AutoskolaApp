import { Component } from '@angular/core';
import { Instructor } from 'src/app/models/instructor';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-instructors',
  templateUrl: './instructors.component.html',
  styleUrls: ['./instructors.component.css'],
})
export class InstructorsComponent {
  allInstructors: any[] = [];
  searchInstructors: string = '';

  selectedInstructor: Instructor;
  pageInstructor = 1;

  showAddPopup: boolean;
  showEditPopup: boolean;

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.fetchData();
    this.showAddPopup = false;
    this.showEditPopup = false;
  }

  public fetchData() {
    this.userService.getInstructors().subscribe((instructors) => {
      this.allInstructors = instructors;
    });
  }

  getInstructors() {
    if (this.allInstructors == null) return [];
    return this.allInstructors.filter((x: any) =>
      x.firstName.toLowerCase().startsWith(this.searchInstructors.toLowerCase())
    );
  }

  addInstructor() {
    this.showAddPopup = true;
  }
  editInstructor(instructor: Instructor) {
    this.selectedInstructor = instructor;
    this.showEditPopup = true;
  }

  addPopupClosed() {
    this.showAddPopup = false;
  }

  editPopupClosed() {
    this.showEditPopup = false;
  }

  changePage(event) {
    this.pageInstructor = event;
  }
}
