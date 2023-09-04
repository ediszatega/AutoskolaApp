import { Component, EventEmitter, Input, Output } from '@angular/core';
import {
  FormGroup,
  FormBuilder,
  Validators,
  ValidatorFn,
  AbstractControl,
  ValidationErrors,
} from '@angular/forms';
import { NgToastService } from 'ng-angular-popup';
import { City } from 'src/app/models/city';
import { Instructor } from 'src/app/models/instructor';
import { CityService } from 'src/app/services/city.service';
import {
  createDateFromFormat,
  dateOfBirthValidator,
  validateDateOfBirth,
} from 'src/app/services/helper/utilities';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-instructor-form',
  templateUrl: './instructor-form.component.html',
  styleUrls: ['./instructor-form.component.css'],
})
export class InstructorFormComponent {
  @Input() instructor: Instructor;
  @Output() submit = new EventEmitter<void>();

  instructorForm!: FormGroup;
  cities: City[];

  constructor(
    private userService: UserService,
    private cityService: CityService,
    private toast: NgToastService,
    private fb: FormBuilder
  ) {
    this.instructorForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', Validators.pattern(/^\d{9}$/)],
      dateOfBirth: ['', [Validators.required, dateOfBirthValidator()]],
      username: ['', Validators.required],
      password: [
        '',
        (control) => {
          if (this.instructor == null) {
            return Validators.required(control);
          } else return null;
        },
      ],
      city: ['', Validators.required],
      drivingLicense: ['', Validators.pattern(/^\d+$/)],
    });
  }

  ngOnInit(): void {
    this.cityService.getCities().subscribe((cities) => {
      this.cities = cities;
    });
    this.instructorForm.reset();
    if (this.instructor != null) this.updateForm();
    console.log(validateDateOfBirth('11/11/2001'));
  }

  updateForm() {
    this.instructorForm.setValue({
      firstName: this.instructor.firstName,
      lastName: this.instructor.lastName,
      email: this.instructor.email,
      phoneNumber: this.instructor.phoneNumber,
      dateOfBirth: this.instructor.dateOfBirth,
      username: this.instructor.username,
      password: '',
      city: this.instructor.city.id,
      drivingLicense: this.instructor.drivingLicense,
    });
  }

  onRemove() {
    if (this.instructor != null) {
      this.userService.removeUser(this.instructor.id).subscribe(() => {
        this.toast.success({
          detail: 'Uspjeh',
          summary: 'Uspješno izbrisan korisnik',
          duration: 5000,
        });
        this.instructorForm.reset();
        this.submit.emit();
      });
    }
  }

  onSubmit() {
    if (this.instructor == null) this.addInstructor();
    else this.editInstructor();
  }

  addInstructor() {
    const formValue = this.instructorForm.value;
    const instructor = {
      firstName: formValue.firstName,
      lastName: formValue.lastName,
      email: formValue.email,
      phoneNumber: formValue.phoneNumber,
      dateOfBirth: createDateFromFormat(formValue.dateOfBirth),
      username: formValue.username,
      password: formValue.password,
      cityId: formValue.city,
      drivingLicense: formValue.drivingLicense,
    };
    this.userService.addInstructor(instructor).subscribe(() => {
      this.toast.success({
        detail: 'Uspjeh',
        summary: 'Uspješno dodan korisnik',
        duration: 5000,
      });
      this.instructorForm.reset();
      this.submit.emit();
    });
  }

  editInstructor() {
    const formValue = this.instructorForm.value;
    const instructor = {
      id: this.instructor.id,
      firstName: formValue.firstName,
      lastName: formValue.lastName,
      email: formValue.email,
      phoneNumber: formValue.phoneNumber,
      dateOfBirth: createDateFromFormat(formValue.dateOfBirth),
      username: formValue.username,
      password: formValue.password,
      cityId: formValue.city,
      drivingLicense: formValue.drivingLicense,
    };
    this.userService.updateInstructor(instructor).subscribe(() => {
      this.toast.success({
        detail: 'Uspjeh',
        summary: 'Uspješno izmijenjen korisnik',
        duration: 5000,
      });
      this.instructorForm.reset();
      this.submit.emit();
    });
  }
}
