import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { BookingService, Booking } from '../../../core/services/booking.service';

@Component({
    selector: 'app-user-bookings',
    standalone: true,
    imports: [CommonModule, RouterModule],
    templateUrl: './user-bookings.component.html',
    styleUrl: './user-bookings.component.css'
})
export class UserBookingsComponent implements OnInit {
    bookings: Booking[] = [];

    constructor(private bookingService: BookingService) { }

    ngOnInit(): void {
        this.loadBookings();
    }

    loadBookings() {
        this.bookingService.getUserBookings().subscribe({
            next: (data) => this.bookings = data,
            error: (err) => console.error('Error loading bookings', err)
        });
    }

    cancelBooking(id: number) {
        if (confirm('Are you sure you want to cancel this booking?')) {
            this.bookingService.cancelBooking(id).subscribe({
                next: () => {
                    this.loadBookings(); // Reload to update status
                },
                error: (err) => console.error('Error cancelling booking', err)
            });
        }
    }
}
