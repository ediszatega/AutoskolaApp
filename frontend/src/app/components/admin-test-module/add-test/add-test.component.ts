import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { TestService } from 'src/app/services/test.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgToastService } from 'ng-angular-popup';
import { Category } from 'src/app/models/category';
import { Test } from 'src/app/models/test';

@Component({
  selector: 'app-add-test',
  templateUrl: './add-test.component.html',
  styleUrls: ['./add-test.component.css'],
})
export class AddTestComponent implements OnInit {
  @Output() submit = new EventEmitter<Test>();

  testForm!: FormGroup;
  categories: Category[] = [];

  constructor(
    private testService: TestService,
    private toast: NgToastService,
    private fb: FormBuilder
  ) {
    this.testForm = this.fb.group({
      description: ['', Validators.required],
      category: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.testService.getCategories().subscribe((categories) => {
      this.categories = categories;
    });
  }

  onTestSubmit() {
    const formValue = this.testForm.value;
    const test = {
      description: formValue.description,
      categoryId: formValue.category,
    };
    this.testService.addTest(test).subscribe((result: Test) => {
      this.toast.success({
        detail: 'Uspjeh',
        summary: 'Uspješno dodan test',
        duration: 5000,
      });
      this.testForm.reset();
      this.submit.emit(result);
    });
  }
}
