import { Component, Input } from '@angular/core';
import { City } from 'src/app/models/city';
import { User } from 'src/app/models/user';
import { convertDate } from 'src/app/services/helper/utilities';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-admins',
  templateUrl: './admins.component.html',
  styleUrls: ['./admins.component.css'],
})
export class AdminsComponent {
  allAdmins: any[] = [];
  searchAdmins: string = '';

  selectedAdmin: User;
  pageAdmin = 1;

  showAddPopup: boolean;
  showEditPopup: boolean;

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.fetchData();
    this.showAddPopup = false;
    this.showEditPopup = false;
  }

  public fetchData() {
    this.userService.getAdmins().subscribe((admins) => {
      console.log(admins);
      this.allAdmins = admins;
    });
  }

  getAdmins() {
    if (this.allAdmins == null) return [];
    return this.allAdmins.filter((x: any) =>
      x.firstName.toLowerCase().startsWith(this.searchAdmins.toLowerCase())
    );
  }

  addAdmin() {
    this.showAddPopup = true;
  }
  editAdmin(admin: User) {
    this.selectedAdmin = admin;
    this.showEditPopup = true;
  }

  addPopupClosed() {
    this.showAddPopup = false;
  }

  editPopupClosed() {
    this.showEditPopup = false;
  }
}
