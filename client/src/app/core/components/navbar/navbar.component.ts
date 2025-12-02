import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { Subject, takeUntil } from 'rxjs';

@Component({
    selector: 'app-navbar',
    standalone: true,
    imports: [CommonModule, RouterLink, RouterLinkActive],
    templateUrl: './navbar.component.html',
    styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit, OnDestroy {
    currentUser: any = null;
    isMenuOpen = false;
    private destroy$ = new Subject<void>();

    constructor(
        private authService: AuthService,
        private router: Router
    ) { }

    ngOnInit(): void {
        this.authService.currentUser$
            .pipe(takeUntil(this.destroy$))
            .subscribe(user => {
                this.currentUser = user;
            });
    }

    ngOnDestroy(): void {
        this.destroy$.next();
        this.destroy$.complete();
    }

    get isAuthenticated(): boolean {
        return this.currentUser !== null;
    }

    get isAdmin(): boolean {
        return this.currentUser?.role === 'Admin';
    }

    get isCustomer(): boolean {
        return this.currentUser?.role === 'Customer';
    }

    toggleMenu(): void {
        this.isMenuOpen = !this.isMenuOpen;
    }

    closeMenu(): void {
        this.isMenuOpen = false;
    }

    logout(): void {
        this.authService.logout();
        this.closeMenu();
    }
}
