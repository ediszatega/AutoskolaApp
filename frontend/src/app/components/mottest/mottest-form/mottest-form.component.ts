import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgToastComponent, NgToastService } from 'ng-angular-popup';
import { MotTest } from 'src/app/models/mottest';
import { Vehicle } from 'src/app/models/vehicle';
import {
  createDateFromFormat,
  dateValidator,
} from 'src/app/services/helper/utilities';
import { MottestService } from 'src/app/services/mottest.service';
import { VehicleService } from 'src/app/services/vehicle.service';

@Component({
  selector: 'app-mottest-form',
  templateUrl: './mottest-form.component.html',
  styleUrls: ['./mottest-form.component.css'],
})
export class MottestFormComponent implements OnInit {
  @Input() mottest: MotTest;
  @Output() submit = new EventEmitter<void>();

  mottestForm: FormGroup;
  vehicles: Vehicle[] = [];

  constructor(
    private mottestService: MottestService,
    private vehicleService: VehicleService,
    private toast: NgToastService,
    private fb: FormBuilder
  ) {
    this.mottestForm = this.fb.group({
      description: ['', Validators.required],
      date: ['', [Validators.required, dateValidator()]],
      mileage: ['', Validators.required],
      vehicle: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.fetchData();
    this.mottestForm.reset();
    if (this.mottest != null) this.updateForm();
  }

  fetchData() {
    this.vehicleService.getVehicles().subscribe((vehicles) => {
      this.vehicles = vehicles;
    });
  }

  updateForm() {
    this.mottestForm.setValue({
      description: this.mottest.description,
      date: this.mottest.date,
      mileage: this.mottest.mileage,
      vehicle: this.mottest.vehicleId,
    });
  }

  onSubmit() {
    if (this.mottest == null) this.addMotTest();
    else this.editMotTest();
  }

  addMotTest() {
    const formValue = this.mottestForm.value;
    const mottest = {
      description: formValue.description,
      date: createDateFromFormat(formValue.date),
      mileage: formValue.mileage,
      vehicleId: formValue.vehicle,
    };

    this.mottestService.addMotTest(mottest).subscribe(() => {
      this.toast.success({
        detail: 'Uspješno',
        summary: 'Uspješno dodan servis',
        duration: 5000,
      });
      this.mottestForm.reset();
      this.submit.emit();
    });
  }

  editMotTest() {
    const formValue = this.mottestForm.value;
    const mottest = {
      id: this.mottest.id,
      description: formValue.description,
      date: createDateFromFormat(formValue.date),
      mileage: formValue.mileage,
      vehicleId: formValue.vehicle,
    };

    this.mottestService.updateMotTest(mottest).subscribe(() => {
      this.toast.success({
        detail: 'Uspješno',
        summary: 'Uspješno izmijenjen servis',
        duration: 5000,
      });
      this.mottestForm.reset();
      this.submit.emit();
    });
  }

  onRemove() {
    if (this.mottest != null) {
      this.mottestService.removeMotTest(this.mottest.id).subscribe(() => {
        this.toast.success({
          detail: 'Uspjeh',
          summary: 'Uspješno izbrisan korisnik',
          duration: 5000,
        });
        this.mottestForm.reset();
        this.submit.emit();
      });
    }
  }
}
