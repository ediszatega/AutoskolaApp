import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgToastService } from 'ng-angular-popup';
import { Customer } from 'src/app/models/customer';
import { Payment } from 'src/app/models/payment';
import {
  createDateFromFormat,
  dateValidator,
} from 'src/app/services/helper/utilities';
import { PaymentService } from 'src/app/services/payment.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-payment-form',
  templateUrl: './payment-form.component.html',
  styleUrls: ['./payment-form.component.css'],
})
export class PaymentFormComponent implements OnInit {
  @Input() payment: Payment;
  @Output() submit = new EventEmitter<void>();

  paymentForm: FormGroup;
  customers: Customer[] = [];

  constructor(
    private paymentService: PaymentService,
    private customerService: UserService,
    private toast: NgToastService,
    private fb: FormBuilder
  ) {
    this.paymentForm = this.fb.group({
      description: ['', Validators.required],
      amount: ['', Validators.required],
      date: ['', [Validators.required, dateValidator()]],
      customer: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.fetchData();
    this.paymentForm.reset();
    if (this.payment != null) this.updateForm();
  }

  fetchData() {
    this.customerService.getCustomers().subscribe((customers) => {
      this.customers = customers;
    });
  }

  updateForm() {
    this.paymentForm.setValue({
      description: this.payment.description,
      amount: this.payment.amount,
      date: this.payment.date,
      customer: this.payment.customerId,
    });
  }

  onSubmit() {
    if (this.payment == null) this.addPayment();
    else this.editPayment();
  }

  addPayment() {
    const formValue = this.paymentForm.value;
    const payment = {
      description: formValue.description,
      amount: formValue.amount,
      date: createDateFromFormat(formValue.date),
      customerId: formValue.customer,
    };

    this.paymentService.addPayment(payment).subscribe(() => {
      this.toast.success({
        detail: 'Uspješno',
        summary: 'Uspješno dodano plaćanje',
        duration: 5000,
      });
      this.paymentForm.reset();
      this.submit.emit();
    });
  }

  editPayment() {
    const formValue = this.paymentForm.value;
    const payment = {
      id: this.payment.id,
      description: formValue.description,
      amount: formValue.amount,
      date: createDateFromFormat(formValue.date),
      customerId: formValue.customer,
    };

    this.paymentService.updatePayment(payment).subscribe(() => {
      this.toast.success({
        detail: 'Uspješno',
        summary: 'Uspješno izmijenjeno plaćanje',
        duration: 5000,
      });
      this.paymentForm.reset();
      this.submit.emit();
    });
  }

  onRemove() {
    if (this.payment != null) {
      this.paymentService.removePayment(this.payment.id).subscribe(() => {
        this.toast.success({
          detail: 'Uspjeh',
          summary: 'Uspješno izbrisano plaćanje',
          duration: 5000,
        });
        this.paymentForm.reset();
        this.submit.emit();
      });
    }
  }
}
