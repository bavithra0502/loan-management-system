import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent {
  userName = '';
  password = '';
  error = '';
  loading = false;

  constructor(private auth: AuthService, private router: Router) {}

  submit(): void {
    this.error = '';
    this.loading = true;
    this.auth.login(this.userName, this.password).subscribe({
      next: res => {
        this.loading = false;
        if (res.status !== 'Approved') {
          this.error = `Your account status is "${res.status}". Please wait for admin approval.`;
          this.auth.logout();
          return;
        }
        if (res.role === 'Admin') this.router.navigate(['/admin']);
        else if (res.role === 'LoanOfficer') this.router.navigate(['/officer']);
        else this.router.navigate(['/customer']);
      },
      error: err => {
        this.loading = false;
        this.error = err.error?.message || 'Login failed. Check your username and password.';
      }
    });
  }
}
