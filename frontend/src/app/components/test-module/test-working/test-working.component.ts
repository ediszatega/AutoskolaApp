import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Answer } from 'src/app/models/answer';
import { Question } from 'src/app/models/question';
import { Test } from 'src/app/models/test';
import { TestService } from 'src/app/services/test.service';

@Component({
  selector: 'app-test-working',
  templateUrl: './test-working.component.html',
  styleUrls: ['./test-working.component.css'],
})
export class TestWorkingComponent {
  current_test_number: number;
  current_test: Test;
  number_of_questions: number;
  number_of_questions_text: string;
  current_question_number: number = 0;
  current_question_number_text: string = (
    this.current_question_number + 1
  ).toString();
  current_question: Question;
  answers: Answer[] = [];
  current_question_text: string;

  results_shown: boolean = false;
  test_finished: boolean = false;

  points: number = 0;
  max_points: number = 0;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private service: TestService
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((paramMap) => {
      let testId = paramMap.get('id');
      if (testId != null) {
        this.service
          .getTestIncludeQuestionsAnswers(parseInt(testId))
          .subscribe((test) => {
            this.current_test = test;
            this.resetAnswers(this.current_test);
            this.number_of_questions = this.current_test.questions.length;
            this.number_of_questions_text = this.number_of_questions.toString();
            this.current_question_number = 0;
            this.current_question_number_text = (
              this.current_question_number + 1
            ).toString();
            this.current_question =
              this.current_test.questions[this.current_question_number];
            this.answers = this.current_question.answers;
            this.current_question_text = this.current_question.text;
            this.results_shown = false;
            this.test_finished = false;
            this.points = 0;
            this.max_points = test.questions.reduce(
              (sum, question) => sum + question.points,
              0
            );
          });
      }
    });
  }

  resetAnswers(current_test: Test): void {
    current_test.questions.forEach((question: Question) => {
      question.answers.forEach((answer: Answer) => {
        answer.class = '';
      });
    });
  }

  selectAnswer(answer: Answer) {
    answer.checked = !answer.checked;
  }

  nextQuestion() {
    if (this.results_shown) this.updateQuestionNext();
    else this.showQuestionResult();
  }

  previousQuestion() {
    this.updateQuestionPrevious();
  }

  showQuestionResult() {
    let number_of_correct_answers = this.current_question.answers.filter(
      (answer) => answer.isCorrect
    ).length;
    let number_of_correct_answers_selected = 0;
    let incorrect_answer = false;
    this.answers.forEach((answer: Answer) => {
      if (answer.isCorrect) {
        answer.class = 'correct';
        if (answer.checked) number_of_correct_answers_selected++;
      } else if (answer.checked) {
        answer.class = 'incorrect';
        incorrect_answer = true;
      }
    });
    if (
      !(
        number_of_correct_answers_selected != number_of_correct_answers ||
        incorrect_answer
      )
    )
      this.points += this.current_question.points;
    this.results_shown = true;
  }

  updateQuestionNext() {
    if (this.current_question_number + 1 < this.number_of_questions) {
      this.current_question_number++;
      this.current_question =
        this.current_test.questions[this.current_question_number];
      this.current_question_number_text = (
        this.current_question_number + 1
      ).toString();
      this.answers = this.current_question.answers;
      this.results_shown = this.answers.some(
        (answer: Answer) => answer.class !== ''
      );
    } else this.finishTest();
  }

  updateQuestionPrevious() {
    if (this.current_question_number > 0) {
      this.current_question_number--;
      this.current_question =
        this.current_test.questions[this.current_question_number];
      this.current_question_number_text = (
        this.current_question_number + 1
      ).toString();
      this.answers = this.current_question.answers;
      this.results_shown = this.answers.some(
        (answer: any) => answer.class !== ''
      );
    }
  }

  updateQuestion(question_number: number) {
    if (!this.results_shown && question_number > this.current_question_number) {
      this.showQuestionResult();
      return;
    }
    if (question_number >= 0 && question_number < this.number_of_questions) {
      this.current_question_number = question_number;
      this.current_question =
        this.current_test.questions[this.current_question_number];
      this.current_question_number_text = (
        this.current_question_number + 1
      ).toString();
      this.answers = this.current_question.answers;
      this.results_shown = this.answers.some(
        (answer: any) => answer.class !== ''
      );
    }
  }

  checkAnswer(answer: Answer) {
    answer.checked = !answer.checked;
  }

  finishTest() {
    this.test_finished = true;
  }
}
