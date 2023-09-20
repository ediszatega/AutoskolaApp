import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ValidationErrors,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { NgToastService } from 'ng-angular-popup';
import { City } from 'src/app/models/city';
import { User } from 'src/app/models/user';
import { CityService } from 'src/app/services/city.service';
import {
  createDateFromFormat,
  dateValidator,
} from 'src/app/services/helper/utilities';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-admin-form',
  templateUrl: './admin-form.component.html',
  styleUrls: ['./admin-form.component.css'],
})
export class AdminFormComponent implements OnInit {
  @Input() admin: User;
  @Output() submit = new EventEmitter<void>();

  adminForm!: FormGroup;
  cities: City[];

  constructor(
    private userService: UserService,
    private cityService: CityService,
    private toast: NgToastService,
    private fb: FormBuilder
  ) {
    this.adminForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', Validators.pattern(/^\d{9}$/)],
      dateOfBirth: ['', [Validators.required, dateValidator()]],
      username: ['', Validators.required],
      password: [
        '',
        (control) => {
          if (this.admin == null) {
            return Validators.required(control);
          } else return null;
        },
      ],
      city: ['', Validators.required],
    });
  }

  passwordValidator(control: AbstractControl): { [key: string]: any } | null {
    if (this.admin == null) {
      return { required: true };
    }
    return null;
  }

  ngOnInit(): void {
    this.cityService.getCities().subscribe((cities) => {
      this.cities = cities;
    });
    this.adminForm.reset();
    if (this.admin != null) this.updateForm();
  }

  updateForm() {
    this.adminForm.setValue({
      firstName: this.admin.firstName,
      lastName: this.admin.lastName,
      email: this.admin.email,
      phoneNumber: this.admin.phoneNumber,
      dateOfBirth: this.admin.dateOfBirth,
      username: this.admin.username,
      password: '',
      city: this.admin.city.id,
    });
  }

  onRemove() {
    console.log(this.admin);
    if (this.admin != null) {
      this.userService.removeUser(this.admin.id).subscribe(() => {
        this.toast.success({
          detail: 'Uspjeh',
          summary: 'Uspješno izbrisan korisnik',
          duration: 5000,
        });
        this.adminForm.reset();
        this.submit.emit();
      });
    }
  }

  onSubmit() {
    if (this.admin == null) this.addAdmin();
    else this.editAdmin();
  }

  addAdmin() {
    const formValue = this.adminForm.value;
    const admin = {
      firstName: formValue.firstName,
      lastName: formValue.lastName,
      email: formValue.email,
      phoneNumber: formValue.phoneNumber,
      dateOfBirth: createDateFromFormat(formValue.dateOfBirth),
      username: formValue.username,
      password: formValue.password,
      cityId: formValue.city,
    };
    this.userService.addAdmin(admin).subscribe(() => {
      this.toast.success({
        detail: 'Uspjeh',
        summary: 'Uspješno dodan korisnik',
        duration: 5000,
      });
      this.adminForm.reset();
      this.submit.emit();
    });
  }

  editAdmin() {
    const formValue = this.adminForm.value;
    const admin = {
      id: this.admin.id,
      firstName: formValue.firstName,
      lastName: formValue.lastName,
      email: formValue.email,
      phoneNumber: formValue.phoneNumber,
      dateOfBirth: createDateFromFormat(formValue.dateOfBirth),
      username: formValue.username,
      password: formValue.password,
      cityId: formValue.city,
    };
    this.userService.updateUser(admin).subscribe(() => {
      this.toast.success({
        detail: 'Uspjeh',
        summary: 'Uspješno izmijenjen korisnik',
        duration: 5000,
      });
      this.adminForm.reset();
      this.submit.emit();
    });
  }
}
