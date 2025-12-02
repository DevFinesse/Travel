import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookingService, Booking } from '../../../core/services/booking.service';
import { ToastService } from '../../../core/services/toast.service';

@Component({
    selector: 'app-booking-list',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './booking-list.component.html',
    styleUrl: './booking-list.component.css'
})
export class BookingListComponent implements OnInit {
    bookings: Booking[] = [];

    constructor(private bookingService: BookingService, private toastService: ToastService) { }

    ngOnInit(): void {
        this.loadBookings();
    }

    loadBookings() {
        this.bookingService.getAllBookings().subscribe({
            next: (data) => this.bookings = data,
            error: (err) => console.error('Error loading bookings', err)
        });
    }

    cancelBooking(id: number) {
        this.bookingService.cancelBooking(id).subscribe({
            next: () => {
                // Update local state
                const booking = this.bookings.find(b => b.id === id);
                if (booking) {
                    booking.status = 'Cancelled';
                }
                this.toastService.show('Booking cancelled successfully', 'success');
            },
            error: (err) => {
                console.error('Error cancelling booking', err);
                this.toastService.show('Failed to cancel booking', 'error');
            }
        });
    }
}
