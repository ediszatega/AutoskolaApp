import { Component, OnInit } from '@angular/core';
import { NgToastService } from 'ng-angular-popup';
import { Payment } from 'src/app/models/payment';
import { PaymentService } from 'src/app/services/payment.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css'],
})
export class PaymentComponent implements OnInit {
  allPayments: Payment[] = [];
  searchPayment: string = '';

  selectedPayment: Payment;
  pagePayment = 1;

  showAddPopup: boolean;
  showEditPopup: boolean;

  constructor(private paymentService: PaymentService) {}

  ngOnInit(): void {
    this.fetchData();
  }

  fetchData() {
    this.paymentService.getPayments().subscribe((payments) => {
      this.allPayments = payments;
    });
  }

  getPayments() {
    if (this.allPayments == null) return [];
    return this.allPayments.filter((x: any) => {
      return x.description
        .toLowerCase()
        .startsWith(this.searchPayment.toLowerCase());
    });
  }

  addPayment() {
    this.showAddPopup = true;
  }

  addPopupClosed() {
    this.showAddPopup = false;
  }

  editPayment(payment: any) {
    console.log(payment);
    this.showEditPopup = true;
    this.selectedPayment = payment;
  }

  editPopupClosed() {
    this.showEditPopup = false;
  }
}
