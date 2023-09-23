import { Component, OnInit } from '@angular/core';
import { Category } from 'src/app/models/category';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css'],
})
export class CategoryComponent implements OnInit {
  allCategories: any[] = [];
  searchCategory: string = '';

  selectedCategory: Category;
  pageCategory = 1;

  showAddPopup: boolean;
  showEditPopup: boolean;

  constructor(private categoryService: CategoryService) {}

  ngOnInit(): void {
    this.fetchData();
  }

  public fetchData() {
    this.categoryService.getCategories().subscribe((category) => {
      this.allCategories = category;
    });
  }

  getCategories() {
    if (this.allCategories == null) return [];
    return this.allCategories.filter((x: any) =>
      x.name.toLowerCase().startsWith(this.searchCategory.toLowerCase())
    );
  }

  addCategory() {
    this.showAddPopup = true;
  }

  editCategory(category: Category) {
    this.selectedCategory = category;
    this.showEditPopup = true;
  }

  addPopupClosed() {
    this.showAddPopup = false;
  }

  editPopupClosed() {
    this.showEditPopup = false;
  }

  onSubmit(category: Category) {
    this.fetchData();
    this.addPopupClosed();
  }
}
