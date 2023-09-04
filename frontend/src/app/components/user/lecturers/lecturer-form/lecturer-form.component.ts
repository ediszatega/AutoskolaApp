import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgToastService } from 'ng-angular-popup';
import { City } from 'src/app/models/city';
import { Lecturer } from 'src/app/models/lecturer';
import { CityService } from 'src/app/services/city.service';
import {
  dateOfBirthValidator,
  validateDateOfBirth,
  createDateFromFormat,
} from 'src/app/services/helper/utilities';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-lecturer-form',
  templateUrl: './lecturer-form.component.html',
  styleUrls: ['./lecturer-form.component.css'],
})
export class LecturerFormComponent implements OnInit {
  @Input() lecturer: Lecturer;
  @Output() submit = new EventEmitter<void>();

  lecturerForm!: FormGroup;
  cities: City[];

  constructor(
    private userService: UserService,
    private cityService: CityService,
    private toast: NgToastService,
    private fb: FormBuilder
  ) {
    this.lecturerForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', Validators.pattern(/^\d{9}$/)],
      dateOfBirth: ['', [Validators.required, dateOfBirthValidator()]],
      username: ['', Validators.required],
      password: [
        '',
        (control) => {
          if (this.lecturer == null) {
            return Validators.required(control);
          } else return null;
        },
      ],
      city: ['', Validators.required],
      degree: ['', Validators.pattern(/^\w+$/)],
    });
  }

  ngOnInit(): void {
    this.cityService.getCities().subscribe((cities) => {
      this.cities = cities;
    });
    this.lecturerForm.reset();
    if (this.lecturer != null) this.updateForm();
  }

  updateForm() {
    this.lecturerForm.setValue({
      firstName: this.lecturer.firstName,
      lastName: this.lecturer.lastName,
      email: this.lecturer.email,
      phoneNumber: this.lecturer.phoneNumber,
      dateOfBirth: this.lecturer.dateOfBirth,
      username: this.lecturer.username,
      password: '',
      city: this.lecturer.city.id,
      degree: this.lecturer.degree,
    });
  }

  onRemove() {
    if (this.lecturer != null) {
      this.userService.removeUser(this.lecturer.id).subscribe(() => {
        this.toast.success({
          detail: 'Uspjeh',
          summary: 'Uspješno izbrisan korisnik',
          duration: 5000,
        });
        this.lecturerForm.reset();
        this.submit.emit();
      });
    }
  }

  onSubmit() {
    if (this.lecturer == null) this.addLecturer();
    else this.editLecturer();
  }

  addLecturer() {
    const formValue = this.lecturerForm.value;
    const lecturer = {
      firstName: formValue.firstName,
      lastName: formValue.lastName,
      email: formValue.email,
      phoneNumber: formValue.phoneNumber,
      dateOfBirth: createDateFromFormat(formValue.dateOfBirth),
      username: formValue.username,
      password: formValue.password,
      cityId: formValue.city,
      degree: formValue.degree,
    };
    this.userService.addLecturer(lecturer).subscribe(() => {
      this.toast.success({
        detail: 'Uspjeh',
        summary: 'Uspješno dodan korisnik',
        duration: 5000,
      });
      this.lecturerForm.reset();
      this.submit.emit();
    });
  }

  editLecturer() {
    const formValue = this.lecturerForm.value;
    const lecturer = {
      id: this.lecturer.id,
      firstName: formValue.firstName,
      lastName: formValue.lastName,
      email: formValue.email,
      phoneNumber: formValue.phoneNumber,
      dateOfBirth: createDateFromFormat(formValue.dateOfBirth),
      username: formValue.username,
      password: formValue.password,
      cityId: formValue.city,
      degree: formValue.degree,
    };
    this.userService.updateLecturer(lecturer).subscribe(() => {
      this.toast.success({
        detail: 'Uspjeh',
        summary: 'Uspješno izmijenjen korisnik',
        duration: 5000,
      });
      this.lecturerForm.reset();
      this.submit.emit();
    });
  }
}
