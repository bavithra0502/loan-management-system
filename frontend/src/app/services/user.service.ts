import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { User } from '../models/user.model';

@Injectable({ providedIn: 'root' })
export class UserService {
  private baseUrl = environment.apiUrl + 'Users/';

  constructor(private http: HttpClient) {}

  getAll(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl);
  }

  getByRole(role: string): Observable<User[]> {
    return this.http.get<User[]>(`${this.baseUrl}role/${role}`);
  }

  getById(id: number): Observable<User> {
    return this.http.get<User>(`${this.baseUrl}${id}`);
  }

  updateStatus(id: number, status: string): Observable<any> {
    return this.http.put(`${this.baseUrl}${id}/status`, JSON.stringify(status), {
      headers: { 'Content-Type': 'application/json' }
    });
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}${id}`);
  }
}
