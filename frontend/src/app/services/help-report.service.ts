import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { HelpReport } from '../models/help-report.model';

@Injectable({ providedIn: 'root' })
export class HelpReportService {
  private baseUrl = environment.apiUrl + 'HelpReports/';

  constructor(private http: HttpClient) {}

  getAll(): Observable<HelpReport[]> {
    return this.http.get<HelpReport[]>(this.baseUrl);
  }

  getById(id: number): Observable<HelpReport> {
    return this.http.get<HelpReport>(`${this.baseUrl}${id}`);
  }

  getByUser(userId: number): Observable<HelpReport[]> {
    return this.http.get<HelpReport[]>(`${this.baseUrl}by-user/${userId}`);
  }

  create(report: Partial<HelpReport>): Observable<HelpReport> {
    return this.http.post<HelpReport>(this.baseUrl, report);
  }

  reply(id: number, reply: string, status: string): Observable<any> {
    return this.http.put(`${this.baseUrl}${id}/reply`, { reply, status });
  }
}
