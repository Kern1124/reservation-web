import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { env } from '../env';
import { Observable } from 'rxjs';
import { GetMyNotificationsResponse, NotificationDto } from 'src/types';

@Injectable({
  providedIn: 'root'
})
export class NotificationsService {
  constructor(private apiService: ApiService) {
  }

  public getMyNotifications = (): Observable<GetMyNotificationsResponse> => {
    return this.apiService.get(`${env.apiUrl}/notifications/my`);
  }

  public setNotificationAsRead = (id: number): Observable<any> => {
    return this.apiService.post(`${env.apiUrl}/notifications/${id}/setAsRead`, {});
  }
}
