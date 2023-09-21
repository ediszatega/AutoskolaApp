import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AngularFireStorage } from '@angular/fire/compat/storage';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgToastService } from 'ng-angular-popup';
import { Observable, finalize } from 'rxjs';
import { Answer } from 'src/app/models/answer';
import { Category } from 'src/app/models/category';
import { Question, QuestionType } from 'src/app/models/question';
import { Test } from 'src/app/models/test';
import { TestService } from 'src/app/services/test.service';
import { v4 as uuidv4 } from 'uuid';

@Component({
  selector: 'app-edit-test',
  templateUrl: './edit-test.component.html',
  styleUrls: ['./edit-test.component.css'],
})
export class EditTestComponent {
  @Input() test: Test;
  @Output() submit = new EventEmitter<Test>();
  @Output() delete = new EventEmitter();

  testForm!: FormGroup;
  questionForm!: FormGroup;
  categories: Category[] = [];

  questions: Question[] = [];
  answers: Answer[] = [];

  newQuestion: boolean = false;

  questionSelected: boolean = false;
  selectedQuestion: Question;

  image: any;
  imageUrl: string = '';

  constructor(
    private testService: TestService,
    private toast: NgToastService,
    private fb: FormBuilder,
    private afStorage: AngularFireStorage
  ) {
    this.testForm = this.fb.group({
      description: ['', Validators.required],
      category: ['', Validators.required],
    });
    this.questionForm = this.fb.group({
      text: ['', Validators.required],
      points: [
        '',
        Validators.compose([Validators.required, Validators.pattern(/^\d+$/)]),
      ],
      questionType: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.testService.getCategories().subscribe((categories) => {
      this.categories = categories;
    });
    this.fetchQuestions();
  }

  fetchQuestions() {
    this.testService.getQuestionsByTest(this.test.id).subscribe((questions) => {
      this.testForm.setValue({
        description: this.test.description,
        category: this.test.category.id,
      });
      this.questions = questions;
      console.log(this.questions);
    });
  }

  onTestSubmit() {
    const formValue = this.testForm.value;
    if (this.test.id == null) {
      const test = {
        description: formValue.description,
        categoryId: formValue.category,
      };
      this.testService.addTest(test).subscribe((test) => {
        this.toast.success({
          detail: 'Uspjeh',
          summary: 'Uspješno dodan test',
          duration: 5000,
        });
        this.testForm.reset();
        this.submit.emit(test);
      });
    } else {
      this.test.description = formValue.description;
      this.test.categoryId = formValue.category;
      this.testService.updateTest(this.test).subscribe(() => {
        this.toast.success({
          detail: 'Uspjeh',
          summary: 'Uspješno izmijenjen test',
          duration: 5000,
        });
        this.testForm.reset();
        this.submit.emit(this.test);
      });
    }
  }

  addNewQuestion() {
    this.newQuestion = true;
    this.selectedQuestion = null;
    this.image = null;
    this.imageUrl = '';
    this.answers = [];
  }

  editQuestion(question: Question) {
    this.questionSelected = true;
    this.selectedQuestion = question;
    this.updateForm();
  }

  updateForm() {
    this.questionForm.setValue({
      text: this.selectedQuestion.text,
      points: this.selectedQuestion.points,
      questionType: this.selectedQuestion.questionType,
    });
    this.imageUrl = this.selectedQuestion.image;
    this.answers = this.selectedQuestion.answers;
    console.log(this.selectedQuestion);
  }

  addNewAnswer() {
    this.answers.push({ text: '', isCorrect: false });
    console.log(this.answers);
  }

  onQuestionSubmit() {
    if (this.questionSelected) {
      this.updateQuestion();
    } else if (this.addNewQuestion) {
      this.addQuestion();
    }
  }

  addQuestion() {
    const file = this.image;
    const filePath = `QuestionImages/${uuidv4()}-${file.name}`;
    const fileRef = this.afStorage.ref(filePath);
    this.afStorage
      .upload(filePath, file)
      .snapshotChanges()
      .pipe(
        finalize(() => {
          const downloadURL = fileRef.getDownloadURL();
          downloadURL.subscribe((url) => {
            if (url) {
              const questionAdd = {
                text: this.questionForm.value.text,
                points: this.questionForm.value.points,
                questionType: parseInt(this.questionForm.value.questionType),
                testId: this.test.id,
                image: this.imageUrl,
              };
              this.testService.addQuestion(questionAdd).subscribe((result) => {
                this.testService
                  .addAnswers(result.id, this.answers)
                  .subscribe(() => {
                    this.toast.success({
                      detail: 'Uspjeh',
                      summary: 'Uspješno dodano pitanje',
                      duration: 5000,
                    });
                    this.fetchQuestions();
                    this.newQuestion = false;
                    this.selectedQuestion = null;
                    this.image = null;
                    this.imageUrl = '';
                    this.answers = [];
                    this.questionForm.reset();
                  });
              });
            }
          });
        })
      )
      .subscribe();
  }

  updateQuestion() {
    const file = this.image;
    const filePath = `QuestionImages/${uuidv4()}-${file.name}`;
    const fileRef = this.afStorage.ref(filePath);
    this.afStorage
      .upload(filePath, file)
      .snapshotChanges()
      .pipe(
        finalize(() => {
          const downloadURL = fileRef.getDownloadURL();
          downloadURL.subscribe((url) => {
            if (url) {
              const questionUpdate = {
                id: this.selectedQuestion.id,
                text: this.questionForm.value.text,
                points: this.questionForm.value.points,
                questionType: parseInt(this.questionForm.value.questionType),
                image: url,
              };
              console.log(questionUpdate);
              this.testService.updateQuestion(questionUpdate).subscribe(() => {
                this.testService
                  .addAnswers(this.selectedQuestion.id, this.answers)
                  .subscribe(() => {
                    this.toast.success({
                      detail: 'Uspjeh',
                      summary: 'Uspješno izmijenjeno pitanje',
                      duration: 5000,
                    });
                    this.fetchQuestions();
                    this.questionSelected = false;
                    this.selectedQuestion = null;
                    this.answers = [];
                    this.imageUrl = '';
                    this.questionForm.reset();
                  });
              });
            }
          });
        })
      )
      .subscribe();
  }

  onQuestionDelete() {
    this.testService.removeQuestion(this.selectedQuestion).subscribe(() => {
      this.toast.success({
        detail: 'Uspjeh',
        summary: 'Uspješno izbrisano pitanje',
        duration: 5000,
      });
      this.fetchQuestions();
      this.questionSelected = false;
      this.selectedQuestion = null;
      this.answers = [];
    });
  }

  removeAnswer(answer: Answer) {
    this.answers = this.answers.filter((a) => a !== answer);
  }

  onTestDelete() {
    this.testService.removeTest(this.test).subscribe(() => {
      this.toast.success({
        detail: 'Uspjeh',
        summary: 'Uspješno izbrisan test',
        duration: 5000,
      });
      this.submit.emit();
    });
  }

  onFilesSelected(event) {
    this.image = event.target.files[0];
  }

  uploadImage() {
    const file = this.image;
    const filePath = `QuestionImages/${uuidv4()}-${file.name}`;
    const fileRef = this.afStorage.ref(filePath);
    return this.afStorage
      .upload(filePath, file)
      .snapshotChanges()
      .pipe(
        finalize(() => {
          const downloadURL = fileRef.getDownloadURL();
          downloadURL.subscribe((url) => {
            if (url) {
              this.imageUrl = url;
              console.log(this.imageUrl);
            }
          });
        })
      );
  }
}
