import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgToastService } from 'ng-angular-popup';
import { News } from 'src/app/models/news';
import { AuthService } from 'src/app/services/auth.service';
import { NewsService } from 'src/app/services/news.service';

@Component({
  selector: 'app-edit-news',
  templateUrl: './edit-news.component.html',
  styleUrls: ['./edit-news.component.css'],
})
export class EditNewsComponent {
  @Input() news: News;
  @Output() submit = new EventEmitter<void>();

  newsForm!: FormGroup;
  selectedFile: File | null = null;
  image: string = '';

  constructor(
    private newsService: NewsService,
    private toast: NgToastService,
    private fb: FormBuilder
  ) {
    this.newsForm = this.fb.group({
      title: ['', Validators.required],
      text: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.newsForm.setValue({
      title: this.news.title,
      text: this.news.text,
    });
    console.log(this.news);
  }

  onSubmit() {
    const formValue = this.newsForm.value;
    const news = {
      title: formValue.title,
      text: formValue.text,
      image: this.image,
    };
    this.newsService.updateNews(news).subscribe(() => {
      this.toast.success({
        detail: 'Uspjeh',
        summary: 'Uspješno izmijenjene novosti',
        duration: 5000,
      });
      this.newsForm.reset();
      this.submit.emit();
    });
  }

  onDelete() {
    this.newsService.deleteNews(this.news).subscribe(() => {
      this.toast.success({
        detail: 'Uspjeh',
        summary: 'Uspješno izbrisane novosti',
        duration: 5000,
      });
      this.newsForm.reset();
      this.submit.emit();
    });
  }

  onFileSelected(event: any) {
    var files = event.target.files;
    var file = files[0];

    if (files && file) {
      var reader = new FileReader();

      reader.onload = this._handleReaderLoaded.bind(this);

      reader.readAsBinaryString(file);
    }
  }

  _handleReaderLoaded(readerEvt) {
    var binaryString = readerEvt.target.result;
    this.image = btoa(binaryString);
  }
}
