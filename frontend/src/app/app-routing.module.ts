import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from './components/login/login.component';
import { RegisterCustomerComponent } from './components/register-customer/register-customer.component';
import { RegisterOfficerComponent } from './components/register-officer/register-officer.component';
import { AdminDashboardComponent } from './components/admin/admin-dashboard.component';
import { CustomerDashboardComponent } from './components/customer/customer-dashboard.component';
import { OfficerDashboardComponent } from './components/officer/officer-dashboard.component';
import { ProfileComponent } from './components/profile/profile.component';
import { ChangePasswordComponent } from './components/change-password/change-password.component';
import { RoleGuard } from './guards/role.guard';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register/customer', component: RegisterCustomerComponent },
  { path: 'register/officer', component: RegisterOfficerComponent },
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
  { path: 'change-password', component: ChangePasswordComponent, canActivate: [AuthGuard] },
  {
    path: 'admin',
    component: AdminDashboardComponent,
    canActivate: [RoleGuard],
    data: { roles: ['Admin'] }
  },
  {
    path: 'customer',
    component: CustomerDashboardComponent,
    canActivate: [RoleGuard],
    data: { roles: ['Customer'] }
  },
  {
    path: 'officer',
    component: OfficerDashboardComponent,
    canActivate: [RoleGuard],
    data: { roles: ['LoanOfficer'] }
  },
  { path: '**', redirectTo: 'login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
