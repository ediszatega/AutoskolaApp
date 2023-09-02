import { Component, Input } from '@angular/core';
import { City } from 'src/app/models/city';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css'],
})
export class CustomersComponent {
  allCustomers: any[] = [];
  searchCustomers: string = '';

  selectedCustomer: User;
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
      console.log(customers);
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
  editCustomer(customer: User) {
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
