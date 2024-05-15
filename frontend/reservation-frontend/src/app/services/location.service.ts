import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
import { env } from '../env';
import { GetCountriesResponse } from 'src/types';

@Injectable({
  providedIn: 'root'
})
export class LocationService {

  constructor(private api: ApiService) { }

  getAllCountries = (): Observable<GetCountriesResponse> => {
    return this.api.get(`${env.apiUrl}/countries/`);
  }
}
