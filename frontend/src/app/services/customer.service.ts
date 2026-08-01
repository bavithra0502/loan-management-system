import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Customer } from '../models/customer.model';

@Injectable({ providedIn: 'root' })
export class CustomerService {
  private baseUrl = environment.apiUrl + 'Customers/';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Customer[]> {
    return this.http.get<Customer[]>(this.baseUrl);
  }

  getById(id: number): Observable<Customer> {
    return this.http.get<Customer>(`${this.baseUrl}${id}`);
  }

  getByUserId(userId: number): Observable<Customer> {
    return this.http.get<Customer>(`${this.baseUrl}by-user/${userId}`);
  }

  update(customer: Customer): Observable<any> {
    return this.http.put(this.baseUrl, customer);
  }
}
