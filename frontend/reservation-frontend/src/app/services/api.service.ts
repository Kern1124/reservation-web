import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { Observable } from 'rxjs';
import { Options } from 'src/types';
import { Form } from '@angular/forms';
@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(
    private httpClient: HttpClient
  ) {}

  get<T>(url: string, options?: Options): Observable<T>{
    return this.httpClient.get(url, options) as Observable<T>;
  } 
  post<T>(url: string, body?: {}, options?: Options): Observable<T>{
    return this.httpClient.post(url, body, options) as Observable<T>;
  }
  put<T>(url: string, body?: {}, options?: Options): Observable<T>{
    return this.httpClient.put(url, body, options) as Observable<T>;
  }
  delete<T>(url: string, options?: Options): Observable<T>{
    return this.httpClient.delete(url, options) as Observable<T>;
  }
  putFile<T>(url: string, file: FormData): Observable<T>{
    return this.httpClient.put(url, file) as Observable<T>;
  }

}
