import { DialogRef } from '@angular/cdk/dialog';
import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ReservationsService } from 'src/app/services/reservations.service';
import { ReservationDto } from 'src/types';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';
import { MatSnackBar } from '@angular/material/snack-bar';
@Component({
  selector: 'app-reservation-list',
  templateUrl: './reservation-list.component.html',
  styleUrls: ['./reservation-list.component.scss']
})
export class ReservationListComponent implements OnInit {
  reservations: ReservationDto[];

  constructor(
    private snackBar: MatSnackBar,
    private dialog: MatDialog,
    private reservationsService: ReservationsService) {}

  ngOnInit(): void {
    this.reservationsService.getMyReservations().subscribe(v => {
      this.reservations = v.reservations;
    });
  }

  onCancel(reservation: ReservationDto): void {
    let dialogRef: MatDialogRef<ConfirmDialogComponent> = this.dialog.open(ConfirmDialogComponent, {
      width: '35%',
      height: '35%',
      data: {
        message: "Are you sure you want to cancel this reservation?", 
      }
    })

    dialogRef.afterClosed().subscribe(confirmed => {
      if (confirmed){
        this.reservationsService.removeReservation(reservation.id).subscribe({
          next: () => this.reservationsService.getMyReservations().subscribe( v => {
            this.snackBar.open("Reservation successfully removed.", "Dismiss", {duration: 3000})
            this.reservations = v.reservations
          }),
        })
      }
    })
  }
}