import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Category } from 'src/app/models/category';
import { Test } from 'src/app/models/test';
import { TestService } from 'src/app/services/test.service';

@Component({
  selector: 'app-test-selection',
  templateUrl: './test-selection.component.html',
  styleUrls: ['./test-selection.component.css'],
})
export class TestSelectionComponent implements OnInit {
  @Input() category!: Category;
  @Output() testEmitter = new EventEmitter<number>();
  tests: Test[] = [];
  selectedTestId: number;
  constructor(private service: TestService) {}

  ngOnInit(): void {
    this.service.getTests(1, 1000, this.category.id).subscribe(
      (tests) => {
        this.tests = tests;
      },
      (error) => {
        console.log('error', error);
      }
    );
  }

  onSelect(event: any) {
    const target = event.target as HTMLSelectElement;
    this.selectedTestId = parseInt(target.value);
    this.service
      .getTestIncludeQuestionsAnswers(this.selectedTestId)
      .subscribe((test) => {
        console.log(test);
      });
  }

  onContinue() {
    if (this.selectedTestId) {
      this.testEmitter.emit(this.selectedTestId);
    }
  }
}
