import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environments';
import { Training } from '../models/training';
import { Observable } from 'rxjs';
import { CreateTrainingDto } from '../models/createTrainingDTO';

@Injectable({
  providedIn: 'root'
})
export class TrainingService {

  private baseUrl = environment.apiUrl + 'trainings';

  constructor(private http: HttpClient) { }

  getAllTrainings(): Observable<Training[]> {
    return this.http.get<Training[]>(this.baseUrl);
  }
  getTrainingById(id: number): Observable<Training> {
    return this.http.get<Training>(`${this.baseUrl}/${id}`);
  }
  getTrainingByUserId(userId: number): Observable<Training[]> {
    return this.http.get<Training[]>(`${this.baseUrl}/by-user/${userId}`);
  }
  getTrainingsByUserAndMonth(userId: number, year: number, month: number): Observable<Training[]> {
    const params = new HttpParams()
      .set('userId', userId.toString())
      .set('year', year.toString())
      .set('month', month.toString());
  
    return this.http.get<Training[]>(`${this.baseUrl}/by-user-and-month`, { params });
  }
  createTraining(training: CreateTrainingDto): Observable<string> {
    return this.http.post(this.baseUrl, training, { responseType: 'text' });
  }
  updateTraining(id: number, training: CreateTrainingDto): Observable<string> {
    return this.http.put(`${this.baseUrl}/${id}`, training, { responseType: 'text' });
  }
  deleteTraining(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
