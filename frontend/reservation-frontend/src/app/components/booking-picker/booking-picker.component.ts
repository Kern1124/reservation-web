import { Component, Inject, Input, OnInit } from '@angular/core';
import { DateAdapter } from '@angular/material/core';
import { MatDialog } from '@angular/material/dialog';
import { formatISO, getDate, getDay } from 'date-fns';
import { OfferedServicesService } from 'src/app/services/offered-services.service';
import { OfferedService, TimeSlotStateDto } from 'src/types';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';
@Component({
  selector: 'app-booking-picker',
  templateUrl: './booking-picker.component.html',
  styleUrls: ['./booking-picker.component.scss']
})
export class BookingPickerComponent implements OnInit {
  @Input() service: OfferedService;
  selectedDate: Date = new Date();
  bookingTerms: TimeSlotStateDto[];
  minDate: Date = new Date()
  maxDate: Date = new Date(new Date().getFullYear() + 1, 12, 31)
  constructor(public dialog: MatDialog, private OSService: OfferedServicesService) { }

  ngOnInit(): void {
    this.fetchBookingTerms();
  }

  fetchBookingTerms(): void {
    let dt = formatISO(this.selectedDate)
    console.log(dt)
    this.OSService.getTimeSlots(this.service.id, dt).subscribe(v => 
      {this.bookingTerms = v.timeSlots
      }
    )
  }

  bookTerm(term: TimeSlotStateDto): void {
    let splitDate = formatISO(this.selectedDate).split('T')
    let startDate: string = splitDate.at(0) + "T" + term.start
    let endDate: string = splitDate.at(0) + "T" + term.end
    if (term.available) {
      let dialogRef = this.dialog.open(ConfirmDialogComponent, {
        width: '35%',
        height: '35%',
        data: {
          message: "Are you sure you want to create this booking?", 
        }
      })
      dialogRef.afterClosed().subscribe(confirmed => {
        if (confirmed){
          this.OSService.createUserReservation(this.service.id, startDate, endDate).subscribe({
            next: () => this.fetchBookingTerms()
          })
        }
      })
    }
  }
}
