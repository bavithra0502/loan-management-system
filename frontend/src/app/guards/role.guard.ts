import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

// Usage in routes: { path: '...', canActivate: [RoleGuard], data: { roles: ['Admin'] } }
@Injectable({ providedIn: 'root' })
export class RoleGuard implements CanActivate {
  constructor(private auth: AuthService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const allowedRoles: string[] = route.data['roles'] ?? [];
    const role = this.auth.getRole();

    if (!this.auth.isLoggedIn()) {
      this.router.navigate(['/login']);
      return false;
    }

    if (allowedRoles.length && (!role || !allowedRoles.includes(role))) {
      this.router.navigate(['/login']);
      return false;
    }

    return true;
  }
}
