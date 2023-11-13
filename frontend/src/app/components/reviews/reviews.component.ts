import { Component, OnInit } from '@angular/core';
import { Instructor } from 'src/app/models/instructor';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-reviews',
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.css'],
})
export class ReviewsComponent implements OnInit {
  employees: any[] = [];

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.userService.getEmployeesWithScore().subscribe((employees) => {
      this.employees = employees.map((employee) => ({
        ...employee,
        score: employee.score == '0' ? 'Nema ocjena' : employee.score,
        role: this.translateRole(employee.role),
      }));
      console.log(employees);
      console.log(this.employees);
    });
  }

  translateRole(role: string) {
    switch (role) {
      case 'Instructor':
        return 'Instruktor';
      case 'Lecturer':
        return 'Predavač';
      default:
        return 'Uposlenik';
    }
  }
}
