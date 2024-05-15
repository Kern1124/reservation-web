import { Component, OnInit } from '@angular/core';
import { ReservationsService } from 'src/app/services/reservations.service';
import { ReservationDto } from 'src/types';
@Component({
  selector: 'app-reservation-list',
  templateUrl: './reservation-list.component.html',
  styleUrls: ['./reservation-list.component.scss']
})
export class ReservationListComponent implements OnInit {
  reservations: ReservationDto[];

  constructor(private reservationsService: ReservationsService) {}

  ngOnInit(): void {
    // this.reservations = this.reservationsService.getReservations();
  }

  onCancel(id: string): void {
    // this.reservationsService.cancelReservation(id);
    // this.reservations = this.reservationsService.getReservations();
  }
}