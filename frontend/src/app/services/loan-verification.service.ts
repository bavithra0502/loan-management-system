import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { LoanVerification } from '../models/loan-verification.model';

@Injectable({ providedIn: 'root' })
export class LoanVerificationService {
  private baseUrl = environment.apiUrl + 'LoanVerifications/';

  constructor(private http: HttpClient) {}

  getAll(): Observable<LoanVerification[]> {
    return this.http.get<LoanVerification[]>(this.baseUrl);
  }

  getById(id: number): Observable<LoanVerification> {
    return this.http.get<LoanVerification>(`${this.baseUrl}${id}`);
  }

  getByOfficer(officerId: number): Observable<LoanVerification[]> {
    return this.http.get<LoanVerification[]>(`${this.baseUrl}by-officer/${officerId}`);
  }

  assign(loanRequestId: number, officerId: number): Observable<LoanVerification> {
    return this.http.post<LoanVerification>(`${this.baseUrl}assign`, { loanRequestId, officerId });
  }

  update(id: number, verificationResult: string, status: string, remarks: string): Observable<any> {
    return this.http.put(`${this.baseUrl}${id}`, { verificationResult, status, remarks });
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}${id}`);
  }
}
