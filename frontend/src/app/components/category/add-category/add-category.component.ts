import { Component, Output, EventEmitter, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { NgToastService } from 'ng-angular-popup';
import { Category } from 'src/app/models/category';
import { CategoryService } from 'src/app/services/category.service';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.css'],
})
export class AddCategoryComponent {
  @Output() submit = new EventEmitter<Category>();

  categoryForm!: FormGroup;

  constructor(
    private categoryService: CategoryService,
    private toast: NgToastService,
    private fb: FormBuilder
  ) {
    this.categoryForm = this.fb.group({
      name: ['', Validators.required],
      description: [''],
    });
  }

  onCategorySubmit() {
    const formValue = this.categoryForm.value;
    const category = {
      name: formValue.name,
      description: formValue.description,
    };

    this.categoryService.addCategory(category).subscribe((result: Category) => {
      this.toast.success({
        detail: 'Uspjeh',
        summary: 'Uspješno dodana kategorija',
        duration: 5000,
      });
      this.categoryForm.reset();
      this.submit.emit(result);
    });
  }
}
