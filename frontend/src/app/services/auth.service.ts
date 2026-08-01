import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { LoginResponse } from '../models/user.model';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  // Matches GET api/Logins/{username}/{password}
  login(userName: string, password: string): Observable<LoginResponse> {
    return this.http
      .get<LoginResponse>(`${this.baseUrl}Logins/${encodeURIComponent(userName)}/${encodeURIComponent(password)}`)
      .pipe(
        tap(res => {
          localStorage.setItem('token', res.token);
          localStorage.setItem('user', JSON.stringify(res));
        })
      );
  }

  registerCustomer(payload: any): Observable<any> {
    return this.http.post(`${this.baseUrl}Logins/register/customer`, payload);
  }

  registerOfficer(payload: any): Observable<any> {
    return this.http.post(`${this.baseUrl}Logins/register/officer`, payload);
  }

  changePassword(payload: { userId: number; oldPassword: string; newPassword: string }): Observable<any> {
    return this.http.post(`${this.baseUrl}Logins/change-password`, payload);
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  getCurrentUser(): LoginResponse | null {
    const raw = localStorage.getItem('user');
    return raw ? JSON.parse(raw) : null;
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  getRole(): string | null {
    return this.getCurrentUser()?.role ?? null;
  }
}
