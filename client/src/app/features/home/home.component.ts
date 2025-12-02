import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { TourService, Tour } from '../../core/services/tour.service';

@Component({
    selector: 'app-home',
    standalone: true,
    imports: [CommonModule, RouterModule],
    templateUrl: './home.component.html',
    styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
    featuredTours: Tour[] = [];

    constructor(private tourService: TourService) { }

    ngOnInit(): void {
        this.loadFeaturedTours();
    }

    loadFeaturedTours() {
        this.tourService.getFeaturedTours().subscribe({
            next: (tours) => this.featuredTours = tours,
            error: (err) => console.error('Error loading featured tours', err)
        });
    }
}
