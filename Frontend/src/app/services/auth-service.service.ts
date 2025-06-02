import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { environment } from '../environments/environments';

@Injectable({
  providedIn: 'root'
})
export class AuthServiceService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }
  
  login(credentials: any): Observable<any> {
    return this.http.post(`${this.apiUrl}auth/login`, credentials);
  }
}
