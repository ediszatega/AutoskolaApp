import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgToastService } from 'ng-angular-popup';
import { City } from 'src/app/models/city';
import { Customer } from 'src/app/models/customer';
import { CityService } from 'src/app/services/city.service';
import {
  createDateFromFormat,
  dateOfBirthValidator,
} from 'src/app/services/helper/utilities';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-customer-form',
  templateUrl: './customer-form.component.html',
  styleUrls: ['./customer-form.component.css'],
})
export class CustomerFormComponent implements OnInit {
  @Input() customer: Customer;
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
      dateOfBirth: ['', [Validators.required, dateOfBirthValidator()]],
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

  onRemove() {
    if (this.customer != null) {
      this.userService.removeUser(this.customer.id).subscribe(() => {
        this.toast.success({
          detail: 'Uspjeh',
          summary: 'Uspješno izbrisan korisnik',
          duration: 5000,
        });
        this.customerForm.reset();
        this.submit.emit();
      });
    }
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
    this.userService.updateCustomer(customer).subscribe(() => {
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
