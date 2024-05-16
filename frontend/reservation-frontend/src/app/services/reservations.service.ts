import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
import { env } from '../env';
import { ReservationListResponse } from 'src/types';

@Injectable({
  providedIn: 'root'
})
export class ReservationsService {

  constructor(private apiService: ApiService){
  }

  getMyReservations = (): Observable<ReservationListResponse> => {
    return this.apiService.get(`${env.apiUrl}/reservations/my`)
  }

  removeReservation = (id: number) => {
    return this.apiService.delete(`${env.apiUrl}/reservations/${id}`)
  }
}
