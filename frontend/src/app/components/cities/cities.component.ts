import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiConfig } from '../../../services/api-config';

@Component({
  selector: 'app-cities',
  templateUrl: './cities.component.html',
  styleUrls: ['./cities.component.css'],
})
export class CitiesComponent implements OnInit {
  data: any;
  selected: any;
  search = '';
  add_city = false;

  constructor(private httpClient: HttpClient) {}

  ngOnInit(): void {
    this.fetchData();
  }

  private fetchData() {
    this.httpClient
      .get(ApiConfig.base_url + '/City/GetAll')
      .subscribe((x: any) => {
        this.data = x;
      });
  }

  getData() {
    if (this.data == null) return [];
    return this.data.filter((x: any) =>
      x.name.toLowerCase().startsWith(this.search.toLowerCase())
    );
  }

  addCity() {
    this.selected = {
      id: 0,
      name: '',
      postalCode: 0,
    };
  }

  save() {
    this.httpClient
      .post(ApiConfig.base_url + '/City/Add', this.selected)
      .subscribe((x: any) => {
        this.fetchData();
        this.addCity();
      });
  }

  cancel() {
    this.selected = null;
    this.fetchData();
  }
}
