import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MottestComponent } from './mottest.component';

describe('MottestComponent', () => {
  let component: MottestComponent;
  let fixture: ComponentFixture<MottestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MottestComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MottestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
