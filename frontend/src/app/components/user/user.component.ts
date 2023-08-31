import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { CityService } from 'src/app/services/city.service';
import { City } from 'src/app/models/city';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
})
export class UserComponent implements OnInit {
  cities: City[];

  constructor(private cityService: CityService) {}

  ngOnInit(): void {
    this.fetchData();
  }

  private fetchData() {
    this.cityService.getCities().subscribe((cities) => {
      this.cities = cities;
    });
  }
}
