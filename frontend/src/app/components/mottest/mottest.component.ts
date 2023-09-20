import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MotTest } from 'src/app/models/mottest';
import { MottestService } from 'src/app/services/mottest.service';

@Component({
  selector: 'app-mottest',
  templateUrl: './mottest.component.html',
  styleUrls: ['./mottest.component.css'],
})
export class MottestComponent implements OnInit {
  allMottests: MotTest[] = [];
  searchMottest: string = '';

  selectedMottest: MotTest;
  pageMottest = 1;

  showAddPopup: boolean;
  showEditPopup: boolean;

  constructor(private mottestService: MottestService) {}

  ngOnInit(): void {
    this.fetchData();
  }

  fetchData() {
    this.mottestService.getMotTests().subscribe((mottests) => {
      this.allMottests = mottests;
    });
  }

  getMotTests() {
    if (this.allMottests == null) return [];
    return this.allMottests.filter((x: any) => {
      return x.description
        .toLowerCase()
        .startsWith(this.searchMottest.toLowerCase());
    });
  }

  addMotTest() {
    this.showAddPopup = true;
  }

  addPopupClosed() {
    this.showAddPopup = false;
  }

  editMotTest(mottest: MotTest) {
    this.showEditPopup = true;
    this.selectedMottest = mottest;
  }

  editPopupClosed() {
    this.showEditPopup = false;
  }
}
