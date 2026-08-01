import { Component } from '@angular/core';
import { Location } from '@angular/common';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html'
})
export class ChangePasswordComponent {
  oldPassword = '';
  newPassword = '';
  confirmPassword = '';
  error = '';
  success = '';

  constructor(private auth: AuthService, private location: Location) {}

  submit(): void {
    this.error = '';
    this.success = '';

    if (this.newPassword !== this.confirmPassword) {
      this.error = 'New password and confirmation do not match.';
      return;
    }

    const user = this.auth.getCurrentUser();
    if (!user) {
      this.error = 'You must be logged in.';
      return;
    }

    this.auth
      .changePassword({ userId: user.userId, oldPassword: this.oldPassword, newPassword: this.newPassword })
      .subscribe({
        next: () => {
          this.success = 'Password changed successfully.';
          this.oldPassword = '';
          this.newPassword = '';
          this.confirmPassword = '';
        },
        error: err => (this.error = err.error?.message || 'Could not change password.')
      });
  }

  goBack(): void {
    this.location.back();
  }
}
