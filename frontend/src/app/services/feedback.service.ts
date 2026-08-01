import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Feedback } from '../models/feedback.model';

@Injectable({ providedIn: 'root' })
export class FeedbackService {
  private baseUrl = environment.apiUrl + 'Feedbacks/';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Feedback[]> {
    return this.http.get<Feedback[]>(this.baseUrl);
  }

  getByCustomer(customerId: number): Observable<Feedback[]> {
    return this.http.get<Feedback[]>(`${this.baseUrl}by-customer/${customerId}`);
  }

  add(feedback: Partial<Feedback>): Observable<Feedback> {
    return this.http.post<Feedback>(this.baseUrl, feedback);
  }
}
