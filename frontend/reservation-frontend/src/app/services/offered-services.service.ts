import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { CreateServiceResponse, GetServicesByOwnerIdResponse, GetServicesResponse, GetTimeSlotsResponse, LocationDto, OfferedService, RemoveServiceResponse, TimeSlotDto, UpdateServiceResponse } from 'src/types';
import { env } from '../env';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OfferedServicesService {

  constructor(private api: ApiService) { }

  public getServices = (): Observable<GetServicesResponse> => {
    return this.api.get(`${env.apiUrl}/services/`)
  }

  public getOwnedServices = (): Observable<GetServicesByOwnerIdResponse> => {
    return this.api.get(`${env.apiUrl}/services/my/`)
  }

  public getTimeSlots = (serviceId: number, date: string): Observable<GetTimeSlotsResponse> => {
    return this.api.get(`${env.apiUrl}/services/${serviceId}/time-slots/${date}`)
  }

  public createService = (name: string, description: string, location: LocationDto, timeSlots: TimeSlotDto[]): Observable<CreateServiceResponse> => {
    return this.api.post(`${env.apiUrl}/services/`, {name: name, description: description, location: location, timeSlots: timeSlots})
  }

  public removeService = (id: number): Observable<RemoveServiceResponse> => {
    return this.api.delete(`${env.apiUrl}/services/remove/${id}`)
  }

  public updateService = (id: number, newName: string | null, newDescription: string | null): Observable<UpdateServiceResponse> => {
    return this.api.put(`${env.apiUrl}/services/update/${id}`, {name: newName, description: newDescription})
  }

  public createUserReservation = (id: number, dateStart: string, dateEnd: string): Observable<any> => {
    return this.api.post(`${env.apiUrl}/reservations/`, {serviceId: id, dateStart: dateStart, dateEnd: dateEnd})
  }

  public getServiceReservations = (id: number): Observable<any> => {
    return this.api.get(`${env.apiUrl}/services/${id}/reservations/`)
  }

  public updateServiceImage = (id: number, file: File): Observable<any> => {
    const formData: FormData = new FormData();
    formData.append("image", file, file.name); 
    return this.api.putFile(`${env.apiUrl}/services/${id}/image/`, formData);
  }
}
