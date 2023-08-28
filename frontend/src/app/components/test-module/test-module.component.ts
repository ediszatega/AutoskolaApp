import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Category } from 'src/app/models/category';
import { Test } from 'src/app/models/test';
import { TestService } from 'src/app/services/test.service';

@Component({
  selector: 'app-test-module',
  templateUrl: './test-module.component.html',
  styleUrls: ['./test-module.component.css'],
})
export class TestModuleComponent implements OnInit {
  title: string;
  category: Category;

  categorySelected: boolean;
  test: Test;
  constructor(private service: TestService, private router: Router) {}

  ngOnInit(): void {
    this.title = 'Odaberite kategoriju';
    this.categorySelected = false;
  }

  handleCategorySelect(category: Category) {
    this.category = category;
    this.categorySelected = true;
    this.title = category.name;
  }

  handleTestSelect(testId: number) {
    this.router.navigateByUrl('/tests/' + testId.toString());
  }
}
