import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgToastService } from 'ng-angular-popup';
import { Category } from 'src/app/models/category';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css'],
})
export class EditCategoryComponent implements OnInit {
  @Input() category: Category;
  @Output() submit = new EventEmitter<void>();

  categoryForm: FormGroup;

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

  ngOnInit(): void {
    this.categoryForm.reset();
    this.updateForm();
  }

  updateForm() {
    this.categoryForm.setValue({
      name: this.category.name,
      description: this.category.description,
    });
  }

  onCategorySubmit() {
    const formValue = this.categoryForm.value;
    const category = {
      id: this.category.id,
      name: formValue.name,
      description: formValue.description,
    };

    this.categoryService.updateCategory(category).subscribe(() => {
      this.toast.success({
        detail: 'Uspjeh',
        summary: 'Uspješno izmjenjena kategorija',
        duration: 5000,
      });
      this.categoryForm.reset();
      this.submit.emit();
    });
  }

  onRemove() {
    console.log(this.category.id);
    if (this.category != null) {
      this.categoryService.removeCategory(this.category.id).subscribe(() => {
        this.toast.success({
          detail: 'Uspjeh',
          summary: 'Uspješno izbrisana kategorija',
          duration: 5000,
        });
        this.categoryForm.reset();
        this.submit.emit();
      });
    }
  }
}
