import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { News } from 'src/app/models/news';
import { AuthService } from 'src/app/services/auth.service';
import { NewsService } from 'src/app/services/news.service';

@Component({
  selector: 'app-news-details',
  templateUrl: './news-details.component.html',
  styleUrls: ['./news-details.component.css'],
})
export class NewsDetailsComponent implements OnInit {
  newsId: string;
  news: News;
  currentImage: string;

  currentNumber: number;
  maxNumber: number;

  constructor(
    private route: ActivatedRoute,
    private newsService: NewsService,
    private router: Router,
    public authService: AuthService
  ) {}

  ngOnInit() {
    this.route.paramMap.subscribe((params) => {
      this.newsId = params.get('id');
      this.loadNews();
    });
  }

  loadNews() {
    this.newsService.getNewsById(this.newsId).subscribe((news) => {
      this.news = news;
    });
  }

  back() {
    this.router.navigateByUrl('/novosti');
  }
}
