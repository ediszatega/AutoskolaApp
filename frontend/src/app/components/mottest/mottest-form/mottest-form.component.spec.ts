import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MottestFormComponent } from './mottest-form.component';

describe('MottestFormComponent', () => {
  let component: MottestFormComponent;
  let fixture: ComponentFixture<MottestFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MottestFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MottestFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
