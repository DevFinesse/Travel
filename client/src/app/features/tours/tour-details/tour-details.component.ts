import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule, Router } from '@angular/router';
import { TourService, Tour } from '../../../core/services/tour.service';
import { BookingService } from '../../../core/services/booking.service';
import { AuthService } from '../../../core/services/auth.service';

@Component({
    selector: 'app-tour-details',
    standalone: true,
    imports: [CommonModule, RouterModule],
    templateUrl: './tour-details.component.html',
    styleUrl: './tour-details.component.css'
})
export class TourDetailsComponent implements OnInit {
    tour: Tour | null = null;
    isLoggedIn = false;
    bookingError: string = '';
    bookingSuccess: string = '';

    constructor(
        private route: ActivatedRoute,
        private tourService: TourService,
        private bookingService: BookingService,
        private authService: AuthService,
        private router: Router
    ) { }

    ngOnInit(): void {
        this.isLoggedIn = this.authService.isAuthenticated();
        this.route.params.subscribe(params => {
            const id = +params['id'];
            if (id) {
                this.loadTour(id);
            }
        });
    }

    loadTour(id: number) {
        this.tourService.getTourById(id).subscribe({
            next: (data) => this.tour = data,
            error: (err) => console.error('Error loading tour', err)
        });
    }

    bookTour() {
        if (!this.isLoggedIn) {
            this.router.navigate(['/login']);
            return;
        }

        if (this.tour) {
            this.bookingService.createBooking({ tourId: this.tour.id }).subscribe({
                next: () => {
                    this.bookingSuccess = 'Booking confirmed!';
                    this.bookingError = '';
                    if (this.tour) this.tour.availableSlots--;
                },
                error: (err) => {
                    this.bookingError = err.error || 'Booking failed';
                    this.bookingSuccess = '';
                }
            });
        }
    }
}
