import { Component } from '@angular/core';
import { News } from 'src/app/models/news';
import { convertDate } from 'src/app/services/helper/utilities';
import { NewsService } from 'src/app/services/news.service';

@Component({
  selector: 'app-admin-news',
  templateUrl: './admin-news.component.html',
  styleUrls: ['./admin-news.component.css'],
})
export class AdminNewsComponent {
  allNews: any[] = [];
  searchNews: string = '';

  selectedNews: News;
  pageNews = 1;

  showAddPopup: boolean;
  showEditPopup: boolean;

  constructor(private newsService: NewsService) {}

  ngOnInit(): void {
    this.fetchData();
    this.showAddPopup = false;
    this.showEditPopup = false;
  }

  public fetchData() {
    this.newsService.getNews().subscribe((news) => {
      this.allNews = news;
    });
  }

  getNews() {
    if (this.allNews == null) return [];
    return this.allNews
      .filter((x: any) =>
        x.title.toLowerCase().startsWith(this.searchNews.toLowerCase())
      )
      .map((news) => ({ ...news, date: convertDate(news.date.toString()) }));
  }

  addNews() {
    this.showAddPopup = true;
  }
  editNews(news: News) {
    this.selectedNews = news;
    this.showEditPopup = true;
  }

  addPopupClosed() {
    this.showAddPopup = false;
  }

  editPopupClosed() {
    this.showEditPopup = false;
  }

  onSubmit(news: News) {
    this.fetchData();
    this.addPopupClosed();
  }
}
