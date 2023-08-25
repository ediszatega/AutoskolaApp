import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestWorkingComponent } from './test-working.component';

describe('TestWorkingComponent', () => {
  let component: TestWorkingComponent;
  let fixture: ComponentFixture<TestWorkingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TestWorkingComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TestWorkingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
