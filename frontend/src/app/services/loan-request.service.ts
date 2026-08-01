import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { LoanRequest } from '../models/loan-request.model';

@Injectable({ providedIn: 'root' })
export class LoanRequestService {
  private baseUrl = environment.apiUrl + 'LoanRequests/';

  constructor(private http: HttpClient) {}

  getAll(): Observable<LoanRequest[]> {
    return this.http.get<LoanRequest[]>(this.baseUrl);
  }

  getById(id: number): Observable<LoanRequest> {
    return this.http.get<LoanRequest>(`${this.baseUrl}${id}`);
  }

  getByCustomer(customerId: number): Observable<LoanRequest[]> {
    return this.http.get<LoanRequest[]>(`${this.baseUrl}by-customer/${customerId}`);
  }

  apply(loanRequest: Partial<LoanRequest>): Observable<LoanRequest> {
    return this.http.post<LoanRequest>(this.baseUrl, loanRequest);
  }

  updateStatus(id: number, status: string): Observable<any> {
    return this.http.put(`${this.baseUrl}${id}/status`, { status });
  }
}
