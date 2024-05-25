import { Component, Inject, ViewChild } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { NgxTimepickerFieldComponent } from 'ngx-material-timepicker';
import { TimePeriod } from 'ngx-material-timepicker/src/app/material-timepicker/models/time-period.enum';
import { TimeSlotDto } from 'src/types';

@Component({
  selector: 'app-time-slot-picker',
  templateUrl: './time-slot-picker.component.html',
  styleUrls: ['./time-slot-picker.component.scss']
})
export class TimeSlotPickerComponent {
  @ViewChild('left') startTimeComponent: NgxTimepickerFieldComponent;
  @ViewChild('right') endTimeComponent: NgxTimepickerFieldComponent;
  timeSlotAddError: boolean = false;
  timeSlotAddOverlap: boolean = false;

  constructor(private dialogRef: MatDialogRef<TimeSlotPickerComponent>, 
    @Inject(MAT_DIALOG_DATA) public timeSlots: TimeSlotDto[]){
  }

  removeTimeSlot = (timeslot: TimeSlotDto) => {
    let index: number = this.timeSlots.indexOf(timeslot);
    this.timeSlots.splice(index, 1)
  }
  onTimeSlotAdd = (time1:string, time2:string) => {
    if ((!time1 || !time2) || time1 == time2){
      this.timeSlotAddError = true;
      return;
    }
    console.log(time1, time2)
    this.timeSlotAddError = false;
    this.timeSlotAddOverlap = false;
    // Check if the timeslots overlap/intersect, if yes, do not allow to add the new timeslot into the array.
    if (this.timeSlots.find(t => (t.start <= time2)  &&  (t.end >= time1))){
      this.timeSlotAddOverlap = true;
    } else {
      this.timeSlots.push({start: time1, end: time2, fullSpan: time1 + " - " + time2})
      this.timeSlots.sort((t1, t2) => t1.end <= t2.start ? -1 : 1)
      this.startTimeComponent.writeValue(time2)
    }
  }

  enforceCorrectPeriod = (left: NgxTimepickerFieldComponent, right: NgxTimepickerFieldComponent) => {
    this.timeSlotAddError = false;
    this.timeSlotAddOverlap = false;
    if (left.timepickerTime >= right.timepickerTime){
      left.writeValue(right.timepickerTime)
    }
  }

  onClose = () => {
    this.dialogRef.close()
  }
}
