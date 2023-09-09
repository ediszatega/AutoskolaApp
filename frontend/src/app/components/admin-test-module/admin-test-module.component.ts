import { Component } from '@angular/core';
import { Test } from 'src/app/models/test';
import { TestService } from 'src/app/services/test.service';

@Component({
  selector: 'app-admin-test-module',
  templateUrl: './admin-test-module.component.html',
  styleUrls: ['./admin-test-module.component.css'],
})
export class AdminTestModuleComponent {
  allTests: any[] = [];
  searchTests: string = '';

  selectedTest: Test;
  pageTest = 1;

  showAddPopup: boolean;
  showEditPopup: boolean;

  constructor(private testService: TestService) {}

  ngOnInit(): void {
    this.fetchData();
    this.showAddPopup = false;
    this.showEditPopup = false;
  }

  public fetchData() {
    this.testService.getTests().subscribe((tests) => {
      this.allTests = tests;
    });
  }

  getTests() {
    if (this.allTests == null) return [];
    return this.allTests.filter((x: any) =>
      x.description.toLowerCase().startsWith(this.searchTests.toLowerCase())
    );
  }

  addTest() {
    this.showAddPopup = true;
  }
  editTest(test: Test) {
    this.selectedTest = test;
    this.showEditPopup = true;
  }

  addPopupClosed() {
    this.showAddPopup = false;
  }

  editPopupClosed() {
    this.showEditPopup = false;
  }

  onSubmit(test: Test) {
    this.fetchData();
    this.addPopupClosed();
  }
}
