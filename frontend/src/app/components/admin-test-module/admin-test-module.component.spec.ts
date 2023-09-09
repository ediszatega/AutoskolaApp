import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminTestModuleComponent } from './admin-test-module.component';

describe('AdminTestModuleComponent', () => {
  let component: AdminTestModuleComponent;
  let fixture: ComponentFixture<AdminTestModuleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminTestModuleComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminTestModuleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
