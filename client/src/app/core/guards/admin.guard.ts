import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';

export const adminGuard: CanActivateFn = (route, state) => {
    const authService = inject(AuthService);
    const router = inject(Router);

    // Check if user is authenticated
    if (!authService.isAuthenticated()) {
        return router.createUrlTree(['/login']);
    }

    // Check if user has admin role
    if (authService.isAdmin()) {
        return true;
    }

    // User is authenticated but not an admin - redirect to home
    console.warn('Access denied: User does not have admin privileges');
    return router.createUrlTree(['/']);
};
