import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { LoanOfficer } from '../models/loan-officer.model';

@Injectable({ providedIn: 'root' })
export class LoanOfficerService {
  private baseUrl = environment.apiUrl + 'LoanOfficers/';

  constructor(private http: HttpClient) {}

  getAll(): Observable<LoanOfficer[]> {
    return this.http.get<LoanOfficer[]>(this.baseUrl);
  }

  getById(id: number): Observable<LoanOfficer> {
    return this.http.get<LoanOfficer>(`${this.baseUrl}${id}`);
  }

  getByUserId(userId: number): Observable<LoanOfficer> {
    return this.http.get<LoanOfficer>(`${this.baseUrl}by-user/${userId}`);
  }

  update(officer: LoanOfficer): Observable<any> {
    return this.http.put(this.baseUrl, officer);
  }
}
