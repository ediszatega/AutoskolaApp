import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { News } from 'src/app/models/news';
import { AuthService } from 'src/app/services/auth.service';
import { convertDate } from 'src/app/services/helper/utilities';
import { NewsService } from 'src/app/services/news.service';

@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.css'],
})
export class NewsComponent {
  newsList: News[] = [];
  currentPage = 1;
  itemsPerPage = 4;

  constructor(
    private newsService: NewsService,
    private router: Router,
    public authService: AuthService
  ) {}

  ngOnInit() {
    this.fetchNews();
  }

  fetchNews() {
    this.newsService.getNews().subscribe((news) => {
      this.newsList = news.sort(
        (a, b) => new Date(b.date).getTime() - new Date(a.date).getTime()
      );
      this.newsList = this.newsList.map((news) => ({
        ...news,
        date: convertDate(news.date.toString()),
      }));
    });
  }

  get pageNumbers(): number[] {
    const pageCount = Math.ceil(this.newsList.length / this.itemsPerPage);
    return Array.from({ length: pageCount }, (_, index) => index + 1);
  }

  changePage(page: number) {
    this.currentPage = page;
  }

  navigateToNewsDetail(newsId) {
    this.router.navigate(['/news/', newsId]);
  }
}
