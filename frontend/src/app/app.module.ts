import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { LoginComponent } from './components/login/login.component';
import { RegisterCustomerComponent } from './components/register-customer/register-customer.component';
import { RegisterOfficerComponent } from './components/register-officer/register-officer.component';
import { AdminDashboardComponent } from './components/admin/admin-dashboard.component';
import { CustomerDashboardComponent } from './components/customer/customer-dashboard.component';
import { OfficerDashboardComponent } from './components/officer/officer-dashboard.component';
import { ProfileComponent } from './components/profile/profile.component';
import { ChangePasswordComponent } from './components/change-password/change-password.component';

import { AuthInterceptor } from './interceptors/auth.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterCustomerComponent,
    RegisterOfficerComponent,
    AdminDashboardComponent,
    CustomerDashboardComponent,
    OfficerDashboardComponent,
    ProfileComponent,
    ChangePasswordComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
