import { Component, Input } from '@angular/core';
import { Customer } from 'src/app/models/customer';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css'],
})
export class CustomersComponent {
  allCustomers: any[] = [];
  searchCustomers: string = '';

  selectedCustomer: Customer;
  pageCustomer = 1;

  showAddPopup: boolean;
  showEditPopup: boolean;

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.fetchData();
    this.showAddPopup = false;
    this.showEditPopup = false;
  }

  public fetchData() {
    this.userService.getCustomers().subscribe((customers) => {
      this.allCustomers = customers;
    });
  }

  getCustomers() {
    if (this.allCustomers == null) return [];
    return this.allCustomers.filter((x: any) =>
      x.firstName.toLowerCase().startsWith(this.searchCustomers.toLowerCase())
    );
  }

  addCustomer() {
    this.showAddPopup = true;
  }
  editCustomer(customer: Customer) {
    this.selectedCustomer = customer;
    this.showEditPopup = true;
  }

  addPopupClosed() {
    this.showAddPopup = false;
  }

  editPopupClosed() {
    this.showEditPopup = false;
  }

  changePage(event) {
    this.pageCustomer = event;
  }
}
