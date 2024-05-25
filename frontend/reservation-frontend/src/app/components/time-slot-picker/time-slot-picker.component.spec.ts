import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TimeSlotPickerComponent } from './time-slot-picker.component';

describe('TimeSlotPickerComponent', () => {
  let component: TimeSlotPickerComponent;
  let fixture: ComponentFixture<TimeSlotPickerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TimeSlotPickerComponent]
    });
    fixture = TestBed.createComponent(TimeSlotPickerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
