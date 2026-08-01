import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { CustomerService } from '../../services/customer.service';
import { LoanOfficerService } from '../../services/loan-officer.service';
import { Customer } from '../../models/customer.model';
import { LoanOfficer } from '../../models/loan-officer.model';
import { LoginResponse } from '../../models/user.model';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html'
})
export class ProfileComponent implements OnInit {
  currentUser: LoginResponse | null = null;
  customer: Customer | null = null;
  officer: LoanOfficer | null = null;
  message = '';
  error = '';

  constructor(
    private auth: AuthService,
    private customerService: CustomerService,
    private loanOfficerService: LoanOfficerService,
    private location: Location
  ) {}

  ngOnInit(): void {
    this.currentUser = this.auth.getCurrentUser();
    if (!this.currentUser) return;

    if (this.currentUser.role === 'Customer') {
      this.customerService.getByUserId(this.currentUser.userId).subscribe(c => (this.customer = c));
    } else if (this.currentUser.role === 'LoanOfficer') {
      this.loanOfficerService.getByUserId(this.currentUser.userId).subscribe(o => (this.officer = o));
    }
  }

  saveCustomer(): void {
    if (!this.customer) return;
    this.error = '';
    this.message = '';
    this.customerService.update(this.customer).subscribe({
      next: () => (this.message = 'Profile updated.'),
      error: err => (this.error = err.error?.message || 'Could not update profile.')
    });
  }

  saveOfficer(): void {
    if (!this.officer) return;
    this.error = '';
    this.message = '';
    this.loanOfficerService.update(this.officer).subscribe({
      next: () => (this.message = 'Profile updated.'),
      error: err => (this.error = err.error?.message || 'Could not update profile.')
    });
  }

  goBack(): void {
    this.location.back();
  }
}
