import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register-officer',
  templateUrl: './register-officer.component.html'
})
export class RegisterOfficerComponent {
  model = {
    userName: '',
    password: '',
    officerName: '',
    phone: '',
    email: '',
    address: '',
    employeeCode: ''
  };
  error = '';
  success = '';

  constructor(private auth: AuthService, private router: Router) {}

  submit(): void {
    this.error = '';
    this.success = '';
    this.auth.registerOfficer(this.model).subscribe({
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
