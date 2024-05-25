import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MAT_SNACK_BAR_DATA, MatSnackBar } from '@angular/material/snack-bar';
import { config } from 'rxjs';
import { OfferedServicesService } from 'src/app/services/offered-services.service';
import { ReservationsService } from 'src/app/services/reservations.service';
import { OfferedService, ReservationDto } from 'src/types';

@Component({
  selector: 'app-service-reservations',
  templateUrl: './service-reservations.component.html',
  styleUrls: ['./service-reservations.component.scss']
})
export class ServiceReservationsComponent {
  serviceReservations: ReservationDto[];
  selected: ReservationDto[];
  constructor(
    private snackBar: MatSnackBar,
    private osService: OfferedServicesService,
    private reservationsService: ReservationsService,
    public dialogRef: MatDialogRef<ServiceReservationsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: OfferedService) {
      this.fetchServiceReservations();
    }
  
  fetchServiceReservations(): void {
    this.osService.getServiceReservations(this.data.id).subscribe(v => {
      this.serviceReservations = v.reservations;
    })
  }
  cancelSelectedReservations(): void {
  // TODO: rework into removeReservationRange (1 api call)
  let index: number = 0;
    this.selected.forEach(r => 
      {
        this.reservationsService.removeReservation(r.id).subscribe(v => {
          if (index == (this.selected.length - 1)){
            this.fetchServiceReservations();
            this.snackBar.open("Selected reservations removed.", "Dismiss", {duration: 3000})
          }
          index++;
        }
        )
      })
      
  }
  closeDialog(): void {
    this.dialogRef.close();
  }
  onNgModelChange($event: any){
  }
}
