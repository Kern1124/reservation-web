import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
import { env } from '../env';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private apiService: ApiService) { }

  getUserReservations = (id: number): Observable<any> => {
    return this.apiService.get(`${env.apiUrl}/users/${id}/reservations/`)
  }
}
