import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register-customer',
  templateUrl: './register-customer.component.html'
})
export class RegisterCustomerComponent {
  model = {
    userName: '',
    password: '',
    customerName: '',
    gender: '',
    dob: '',
    phone: '',
    email: '',
    address: '',
    aadhaarNumber: '',
    panNumber: '',
    occupation: '',
    annualIncome: 0
  };
  error = '';
  success = '';

  constructor(private auth: AuthService, private router: Router) {}

  submit(): void {
    this.error = '';
    this.success = '';
    this.auth.registerCustomer(this.model).subscribe({
      next: () => {
        this.success = 'Registration submitted! Wait for admin approval, then log in.';
        setTimeout(() => this.router.navigate(['/login']), 2000);
      },
      error: err => {
        this.error = err.error?.message || 'Registration failed.';
      }
    });
  }
}
