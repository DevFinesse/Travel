import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface Booking {
    id: number;
    userId: number;
    userName: string;
    tourId: number;
    tourName: string;
    bookingDate: string;
    status: string;
}

export interface CreateBookingDto {
    tourId: number;
}

@Injectable({
    providedIn: 'root'
})
export class BookingService {
    private apiUrl = `${environment.apiUrl}/bookings`;

    constructor(private http: HttpClient) { }

    createBooking(booking: CreateBookingDto): Observable<Booking> {
        return this.http.post<Booking>(this.apiUrl, booking);
    }

    getUserBookings(): Observable<Booking[]> {
        return this.http.get<Booking[]>(`${this.apiUrl}/my-bookings`);
    }

    getAllBookings(): Observable<Booking[]> {
        return this.http.get<Booking[]>(this.apiUrl);
    }

    cancelBooking(id: number): Observable<void> {
        return this.http.post<void>(`${this.apiUrl}/${id}/cancel`, {});
    }
}
