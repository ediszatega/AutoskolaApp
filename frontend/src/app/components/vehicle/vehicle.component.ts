import { Component, OnInit } from '@angular/core';
import { Vehicle } from 'src/app/models/vehicle';
import { VehicleService } from 'src/app/services/vehicle.service';

@Component({
  selector: 'app-vehicle',
  templateUrl: './vehicle.component.html',
  styleUrls: ['./vehicle.component.css'],
})
export class VehicleComponent implements OnInit {
  allVehicles: any[] = [];
  searchVehicle: string = '';

  selectedVehicle: Vehicle;
  pageVehicle = 1;

  showAddPopup: boolean;
  showEditPopup: boolean;

  constructor(private vehicleService: VehicleService) {}

  ngOnInit(): void {
    this.fetchData();
  }

  fetchData() {
    this.vehicleService.getVehicles().subscribe((vehicle) => {
      this.allVehicles = vehicle;
    });
  }

  getVehicles() {
    if (this.allVehicles == null) return [];
    return this.allVehicles.filter((x: any) => {
      return x.model.toLowerCase().startsWith(this.searchVehicle.toLowerCase());
    });
  }

  addVehicle() {
    this.showAddPopup = true;
  }

  addPopupClosed() {
    this.showAddPopup = false;
  }

  onSubmit() {
    this.fetchData();
    this.showAddPopup = false;
  }

  editVehicle(vehicle: Vehicle) {
    this.selectedVehicle = vehicle;
    this.showEditPopup = true;
  }

  editPopupClosed() {
    this.showEditPopup = false;
  }
}
