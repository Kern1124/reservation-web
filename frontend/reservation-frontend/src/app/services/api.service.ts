import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { Observable } from 'rxjs';
import { Options } from 'src/types';
@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(
    private httpClient: HttpClient
  ) {}

  get<T>(url: string, body?: {}, options?: Options): Observable<T>{
    return this.httpClient.get(url, options) as Observable<T>;
  } 
  post<T>(url: string, body?: {}, options?: Options): Observable<T>{
    return this.httpClient.post(url, body, options) as Observable<T>;
  }
}
