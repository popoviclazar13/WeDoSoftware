import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environments';
import { Observable } from 'rxjs';
import { User } from '../models/user';
import { CreateUserDto } from '../models/createUserDTO';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private baseUrl = environment.apiUrl + 'users';

  constructor(private http: HttpClient) { }

  getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl);
  }
  getUserById(id: number): Observable<User> {
    return this.http.get<User>(`${this.baseUrl}/${id}`);
  }
  createUser(user: CreateUserDto): Observable<User> {
    return this.http.post<User>(this.baseUrl, user);
  }
  updateUser(id: number, user: CreateUserDto): Observable<User> {
    return this.http.put<User>(`${this.baseUrl}/${id}`, user);
  }
  deleteUser(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
