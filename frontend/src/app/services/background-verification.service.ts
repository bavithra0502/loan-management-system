import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { BackgroundVerification } from '../models/background-verification.model';

@Injectable({ providedIn: 'root' })
export class BackgroundVerificationService {
  private baseUrl = environment.apiUrl + 'BackgroundVerifications/';

  constructor(private http: HttpClient) {}

  getAll(): Observable<BackgroundVerification[]> {
    return this.http.get<BackgroundVerification[]>(this.baseUrl);
  }

  getById(id: number): Observable<BackgroundVerification> {
    return this.http.get<BackgroundVerification>(`${this.baseUrl}${id}`);
  }

  getByOfficer(officerId: number): Observable<BackgroundVerification[]> {
    return this.http.get<BackgroundVerification[]>(`${this.baseUrl}by-officer/${officerId}`);
  }

  assign(loanRequestId: number, officerId: number): Observable<BackgroundVerification> {
    return this.http.post<BackgroundVerification>(`${this.baseUrl}assign`, { loanRequestId, officerId });
  }

  update(id: number, status: string, remarks: string): Observable<any> {
    return this.http.put(`${this.baseUrl}${id}`, { status, remarks });
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}${id}`);
  }
}
