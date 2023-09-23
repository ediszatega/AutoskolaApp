import { Component, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgToastService } from 'ng-angular-popup';
import { AuthService } from 'src/app/services/auth.service';
import { NewsService } from 'src/app/services/news.service';

@Component({
  selector: 'app-add-news',
  templateUrl: './add-news.component.html',
  styleUrls: ['./add-news.component.css'],
})
export class AddNewsComponent {
  @Output() submit = new EventEmitter<void>();

  newsForm!: FormGroup;
  selectedFile: File | null = null;
  image: string = '';
  constructor(
    private newsService: NewsService,
    private authService: AuthService,
    private toast: NgToastService,
    private fb: FormBuilder
  ) {
    this.newsForm = this.fb.group({
      title: ['', Validators.required],
      text: ['', Validators.required],
    });
  }

  ngOnInit(): void {}

  onSubmit() {
    const formValue = this.newsForm.value;
    const user = this.authService.getUser();
    const news = {
      title: formValue.title,
      text: formValue.text,
      image: this.image,
      date: new Date(),
      userId: user.id,
    };
    console.log(news);
    this.newsService.addNews(news).subscribe(() => {
      this.toast.success({
        detail: 'Uspjeh',
        summary: 'Uspješno dodane novosti',
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
