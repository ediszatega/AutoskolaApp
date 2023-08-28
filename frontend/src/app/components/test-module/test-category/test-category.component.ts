import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Category } from 'src/app/models/category';
import { TestService } from 'src/app/services/test.service';

@Component({
  selector: 'app-test-category',
  templateUrl: './test-category.component.html',
  styleUrls: ['./test-category.component.css'],
})
export class TestCategoryComponent implements OnInit {
  @Output() categoryEmitter = new EventEmitter<Category>();

  categories: Category[] = [];
  constructor(private service: TestService) {}
  ngOnInit(): void {
    this.service.getCategories().subscribe((categories) => {
      this.categories = categories;
    });
  }

  selectCategory(category: Category) {
    this.categoryEmitter.emit(category);
  }
}
