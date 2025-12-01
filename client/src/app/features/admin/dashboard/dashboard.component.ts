import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AnalyticsService, AnalyticsDto } from '../../../core/services/analytics.service';

@Component({
    selector: 'app-dashboard',
    standalone: true,
    imports: [CommonModule, RouterModule],
    templateUrl: './dashboard.component.html',
    styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit {
    analytics: AnalyticsDto | null = null;

    constructor(private analyticsService: AnalyticsService) { }

    ngOnInit(): void {
        this.loadAnalytics();
    }

    loadAnalytics() {
        this.analyticsService.getAnalytics().subscribe({
            next: (data) => this.analytics = data,
            error: (err) => console.error('Error loading analytics', err)
        });
    }

    getBarHeight(count: number): string {
        if (!this.analytics || this.analytics.bookingsPerMonth.length === 0) return '0%';
        const max = Math.max(...this.analytics.bookingsPerMonth.map(m => m.count));
        if (max === 0) return '0%';
        return `${(count / max) * 100}%`;
    }
}
