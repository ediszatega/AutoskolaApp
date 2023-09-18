import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgToastService } from 'ng-angular-popup';
import { Category } from 'src/app/models/category';
import { CategoryService } from 'src/app/services/category.service';
import { VehicleService } from 'src/app/services/vehicle.service';
import { Vehicle } from 'src/app/models/vehicle';

@Component({
  selector: 'app-add-vehicle',
  templateUrl: './add-vehicle.component.html',
  styleUrls: ['./add-vehicle.component.css'],
})
export class AddVehicleComponent implements OnInit {
  @Output() submit = new EventEmitter<Vehicle>();

  vehicleForm: FormGroup;

  categories: Category[] = [];

  constructor(
    private vehicleService: VehicleService,
    private categoryService: CategoryService,
    private toast: NgToastService,
    private fb: FormBuilder
  ) {
    this.vehicleForm = this.fb.group({
      model: ['', Validators.required],
      make: ['', Validators.required],
      registration: ['', Validators.required],
      category: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.fetchData();
  }

  fetchData() {
    this.categoryService.getCategories().subscribe((categories) => {
      this.categories = categories;
    });
  }

  onVehicleSubmit() {
    const formValue = this.vehicleForm.value;
    const vehicle = {
      model: formValue.model,
      make: formValue.make,
      registration: formValue.registration,
      categoryId: formValue.category,
    };

    this.vehicleService.addVehicle(vehicle).subscribe(() => {
      this.toast.success({
        detail: 'Uspješno',
        summary: 'Uspješno dodano vozilo',
        duration: 5000,
      });
      this.vehicleForm.reset();
      this.submit.emit();
    });
  }
}
