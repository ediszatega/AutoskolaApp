import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  FormGroup,
  FormBuilder,
  Validators,
  AbstractControl,
} from '@angular/forms';
import { NgToastService } from 'ng-angular-popup';
import { City } from 'src/app/models/city';
import { User } from 'src/app/models/user';
import { CityService } from 'src/app/services/city.service';
import { createDateFromFormat } from 'src/app/services/helper/utilities';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-customer-form',
  templateUrl: './customer-form.component.html',
  styleUrls: ['./customer-form.component.css'],
})
export class CustomerFormComponent implements OnInit {
  @Input() customer: User;
  @Output() submit = new EventEmitter<void>();

  customerForm!: FormGroup;
  cities: City[];

  constructor(
    private userService: UserService,
    private cityService: CityService,
    private toast: NgToastService,
    private fb: FormBuilder
  ) {
    this.customerForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', Validators.pattern(/^\d{9}$/)],
      dateOfBirth: [
        '',
        [Validators.required, Validators.pattern(/^\d{2}\/\d{2}\/\d{4}$/)],
      ],
      username: ['', Validators.required],
      password: [
        '',
        (control) => {
          if (this.customer == null) {
            return Validators.required(control);
          } else return null;
        },
      ],
      city: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.cityService.getCities().subscribe((cities) => {
      this.cities = cities;
    });
    this.customerForm.reset();
    if (this.customer != null) this.updateForm();
  }

  updateForm() {
    this.customerForm.setValue({
      firstName: this.customer.firstName,
      lastName: this.customer.lastName,
      email: this.customer.email,
      phoneNumber: this.customer.phoneNumber,
      dateOfBirth: this.customer.dateOfBirth,
      username: this.customer.username,
      password: '',
      city: this.customer.city.id,
    });
  }

  onSubmit() {
    if (this.customer == null) this.addCustomer();
    else this.editCustomer();
  }

  addCustomer() {
    const formValue = this.customerForm.value;
    const customer = {
      firstName: formValue.firstName,
      lastName: formValue.lastName,
      email: formValue.email,
      phoneNumber: formValue.phoneNumber,
      dateOfBirth: createDateFromFormat(formValue.dateOfBirth),
      username: formValue.username,
      password: formValue.password,
      cityId: formValue.city,
    };
    console.log(customer);
    this.userService.addCustomer(customer).subscribe(() => {
      this.toast.success({
        detail: 'Uspjeh',
        summary: 'Uspješno dodan korisnik',
        duration: 5000,
      });
      this.customerForm.reset();
      this.submit.emit();
    });
  }

  editCustomer() {
    const formValue = this.customerForm.value;
    const customer = {
      id: this.customer.id,
      firstName: formValue.firstName,
      lastName: formValue.lastName,
      email: formValue.email,
      phoneNumber: formValue.phoneNumber,
      dateOfBirth: createDateFromFormat(formValue.dateOfBirth),
      username: formValue.username,
      password: formValue.password,
      cityId: formValue.city,
    };
    console.log(customer);
    this.userService.updateUser(customer).subscribe(() => {
      this.toast.success({
        detail: 'Uspjeh',
        summary: 'Uspješno izmijenjen korisnik',
        duration: 5000,
      });
      this.customerForm.reset();
      this.submit.emit();
    });
  }
}
