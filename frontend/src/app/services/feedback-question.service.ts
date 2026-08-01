import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { FeedbackQuestion } from '../models/feedback-question.model';

@Injectable({ providedIn: 'root' })
export class FeedbackQuestionService {
  private baseUrl = environment.apiUrl + 'FeedbackQuestions/';

  constructor(private http: HttpClient) {}

  getAll(): Observable<FeedbackQuestion[]> {
    return this.http.get<FeedbackQuestion[]>(this.baseUrl);
  }

  getActive(): Observable<FeedbackQuestion[]> {
    return this.http.get<FeedbackQuestion[]>(`${this.baseUrl}active`);
  }

  getById(id: number): Observable<FeedbackQuestion> {
    return this.http.get<FeedbackQuestion>(`${this.baseUrl}${id}`);
  }

  add(question: Partial<FeedbackQuestion>): Observable<FeedbackQuestion> {
    return this.http.post<FeedbackQuestion>(this.baseUrl, question);
  }

  update(question: FeedbackQuestion): Observable<any> {
    return this.http.put(this.baseUrl, question);
  }
}
