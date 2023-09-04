import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LecturerFormComponent } from './lecturer-form.component';

describe('LecturerFormComponent', () => {
  let component: LecturerFormComponent;
  let fixture: ComponentFixture<LecturerFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LecturerFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LecturerFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
