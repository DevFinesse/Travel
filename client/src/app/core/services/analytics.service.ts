import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface MonthlyBooking {
    month: string;
    count: number;
}

export interface AnalyticsDto {
    totalBookings: number;
    totalRevenue: number;
    totalTours: number;
    bookingsPerMonth: MonthlyBooking[];
}

@Injectable({
    providedIn: 'root'
})
export class AnalyticsService {
    private apiUrl = `${environment.apiUrl}/analytics`;

    constructor(private http: HttpClient) { }

    getAnalytics(): Observable<AnalyticsDto> {
        return this.http.get<AnalyticsDto>(this.apiUrl);
    }
}
