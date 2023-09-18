import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgToastService } from 'ng-angular-popup';
import { Category } from 'src/app/models/category';
import { Vehicle } from 'src/app/models/vehicle';
import { CategoryService } from 'src/app/services/category.service';
import { VehicleService } from 'src/app/services/vehicle.service';

@Component({
  selector: 'app-edit-vehicle',
  templateUrl: './edit-vehicle.component.html',
  styleUrls: ['./edit-vehicle.component.css'],
})
export class EditVehicleComponent implements OnInit {
  @Input() vehicle: Vehicle;
  @Output() submit = new EventEmitter<void>();

  categories: Category[] = [];

  vehicleForm: FormGroup;

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
    this.updateForm();
  }

  fetchData() {
    this.categoryService.getCategories().subscribe((categories) => {
      this.categories = categories;
    });
  }

  updateForm() {
    this.vehicleForm.setValue({
      model: this.vehicle.model,
      make: this.vehicle.make,
      registration: this.vehicle.registration,
      category: this.vehicle.categoryId,
    });
  }

  onVehicleSubmit() {
    const formValue = this.vehicleForm.value;
    const vehicle = {
      id: this.vehicle.id,
      model: formValue.model,
      make: formValue.make,
      registration: formValue.registration,
      categoryId: formValue.category,
    };

    this.vehicleService.updateVehicle(vehicle).subscribe(() => {
      this.toast.success({
        detail: 'Uspješno',
        summary: 'Uspješno uređeno vozilo',
        duration: 5000,
      });
      this.vehicleForm.reset();
      this.submit.emit();
    });
  }

  onRemove() {
    if (this.vehicle != null) {
      this.vehicleService.removeVehicle(this.vehicle.id).subscribe(() => {
        this.toast.success({
          detail: 'Uspješno',
          summary: 'Uspješno izbrisano vozilo',
          duration: 5000,
        });
        this.vehicleForm.reset();
        this.submit.emit();
      });
    }
  }
}
